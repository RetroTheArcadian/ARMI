import { Component, OnInit, HostBinding, ViewChild, TemplateRef } from '@angular/core';

import { ColorScheme } from '../app.constants';
import { slideInDownAnimation } from '../utils/animation.utils';
import { Rom, RomList, TreeViewNodeDto, ProgressDto, JobStatus } from '../interfaces/_Models';
import { RomsService } from '../services/roms.service';
import { RomListService } from '../services/romlist.service';
import { ImportService } from '../services/import.service';
import { JobHubService } from '../services/jobHub.service';
import { LoadingService } from '../loading/loading.interface';

import { TreeNode, TreeModel, TREE_ACTIONS, KEYS, IActionMapping, ITreeOptions } from 'angular-tree-component';
import { ContextMenuComponent, ContextMenuService } from 'ngx-contextmenu';
import { NgbDropdownConfig } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-roms',
  templateUrl: './romlists.component.html',
  animations: [slideInDownAnimation],
  providers: [NgbDropdownConfig]
})
export class RomListsComponent implements OnInit {
  @HostBinding('@routeAnimation') routeAnimation = true;
  @HostBinding('style.display') display = 'block';
  @ViewChild(ContextMenuComponent ) public romListMenu: ContextMenuComponent;

  constructor(
    public romListService: RomListService,
    public importService: ImportService,
    public romsService: RomsService,
    public colorScheme: ColorScheme,
    public contextMenuService: ContextMenuService,
    public jobHubService: JobHubService,
    public loadingService: LoadingService,
    config: NgbDropdownConfig
    )
  {
    // customize default values of dropdowns used by this component tree
    config.autoClose = false;
  }
  roms: Rom[];
  page = 1;
  pageSize = 100;
  collectionSize = 0;
  romLists: RomList[];
  romlistNodes: TreeViewNodeDto[] = [];
  categoryModel = {
    left: true,
    middle: false,
    right: false
  };
  options: ITreeOptions = {
    getChildren: this.getRomListsByParentId.bind(this),
    useCheckbox: false,
    allowDrag: (node) => node.isLeaf,
    getNodeClone: (node) => ({
      ...node.data,
      id: node.data.id,
      name: `copy of ${node.data.name}`
    }),
    actionMapping:
    {
      mouse: {
        contextMenu: (tree, node, $event) => {
          this.contextMenuService.show.next({
            contextMenu: this.romListMenu,
            event: $event,
            item: node
          });
          $event.preventDefault();
          $event.stopPropagation();
          //alert(`context menu for ${node.data.name}`);
        },
        dblClick: (tree, node, $event) => {
          if (node.hasChildren) {
            TREE_ACTIONS.TOGGLE_EXPANDED(tree, node, $event);
          }
        },
        click: (tree, node, $event) => {
          this.getRoms(node.id);
          //$event.shiftKey
          //  ? TREE_ACTIONS.TOGGLE_ACTIVE_MULTI(tree, node, $event)
          //  : TREE_ACTIONS.TOGGLE_ACTIVE(tree, node, $event);
        }
      },
      keys: {
        [KEYS.ENTER]: (tree, node, $event) => alert(`This is ${node.data.name}`)
      }
    }
  };
  rootRomList: TreeViewNodeDto = { id: -1, name: "root", hasChildren:true };
  ngOnInit(): void {
    this.initRomLists(this.rootRomList);
  }
  getRomLists() {
    this.loadingService.show();
    this.romListService.romLists().subscribe(romLists => {
      this.romLists = romLists;
      this.loadingService.hide();
    });
  }
  initRomLists(parent: TreeViewNodeDto) {
    this.loadingService.show();
    this.romListService.romListsByParentId(parent.id).subscribe(res => {
      this.romlistNodes = res;
      this.loadingService.hide();
    });
  }
  getRomListsByParentId(parent: TreeViewNodeDto) {
    return this.romListService.romListsByParentId(parent.id).toPromise();
  }
  get romsTable(): Rom[] {
    return this.roms.slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }
  getRoms(romListId: number) {
    this.roms = null;
    this.loadingService.show();
    return this.romsService.romsWithFileInfo([romListId]).subscribe(roms => {
      this.roms = roms;      
      this.collectionSize = roms.length;
      this.loadingService.hide();
    })
  }
  importGameLists() {
    this.roms = [];
    this.romLists = [];
    this.loadingService.show();
    this.importService.importRomLists().subscribe(jobIdGuid => {
      this.jobHubService.watchJob(jobIdGuid).subscribe(progress => {
        this.loadingService.update(progress);
        if (progress.jobStatus != JobStatus.running) {
          this.loadingService.hide();
          this.initRomLists(this.rootRomList);
        }
      });
    })
  }
}
