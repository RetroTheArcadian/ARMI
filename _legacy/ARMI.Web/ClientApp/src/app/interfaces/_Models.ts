
 export class ArcadeDbDto  {
     release: number;
     result: ArcadeDbResultDto[];
 }
 
 
 export class ArcadeDbResultDto  {
     index: number;
     url: string;
     game_name: string;
     title: string;
     cloneof: string;
     manufacturer: string;
     url_image_ingame: string;
     url_image_title: string;
     url_image_marquee: string;
     url_image_cabinet: string;
     url_image_flyer: string;
     genre: string;
     players: number;
     year: string;
     status: string;
     history: string;
     history_copyright_long: string;
     history_copyright_short: string;
     youtube_video_id: string;
     url_video_shortplay: string;
     url_video_shortplay_hd: string;
     emulator_id: number;
     emulator_name: string;
     languages: string;
     rate: number;
 }
 
 
 export class Client  {
     clientId: number;
     title: string;
     clientHostType: ClientHostType;
     host: string;
     port: number;
     userName: string;
     password: string;
     isMaster: boolean;
     romFolder: string;
     attractModeFolder: string;
     emulators: Emulator[];
     romLists: RomList[];
 }
 
 
 export enum ClientHostType {
   local = 0,
   sFtp = 1,
   ftp = 2,
   samba = 3,
 }
 
 
 
 export class EmulationStationGameDto  {
     desc: string;
     developer: string;
     genre: string;
     id: string;
     image: string;
     lastplayed: string;
     marquee: string;
     name: string;
     path: string;
     playcount: string;
     players: string;
     publisher: string;
     rating: string;
     releasedate: string;
     source: string;
     thumbnail: string;
     video: string;
 }
 
 
 export class EmulationStationGameListDto  {
     game: EmulationStationGameDto[];
     folderName: string;
 }
 
 
 export class Emulator  {
     emulatorId: number;
     emulatorName: string;
     executable: string;
     args: string;
     rompath: string;
     romext: string;
     infoSource: string;
     boxart: string;
     cartart: string;
     fanart: string;
     flyer: string;
     marquee: string;
     snap: string;
     wheel: string;
     modifiedDate: Date;
     systemId: number;
     system: System;
     romFolderName: string;
     roms: Rom[];
     clients: Client[];
 }
 
 
 export class Job  {
     jobId: number;
     jobIdGuid: string;
     title: string;
     jobStatus: JobStatus;
     start: Date;
     end: Date;
 }
 
 
 export enum JobStatus {
   queued = 0,
   running = 1,
   cancelled = 2,
   failed = 3,
   done = 4,
 }
 
 
 
 export class ProgressDto  {
     percent: number;
     done: number;
     remaining: number;
     total: number;
     msgLine1: string;
     msgLine2: string;
     jobStatus: JobStatus;
 }
 
 
 export class Rom  {
     romId: number;
     name: string;
     title: string;
     cloneOf: string;
     year: string;
     manufacturer: string;
     category: string;
     players: string;
     rotation: string;
     control: string;
     status: string;
     displayCount: string;
     displayType: string;
     altRomname: string;
     altTitle: string;
     extra: string;
     buttons: string;
     description: string;
     modifiedDate: Date;
     emulatorId: number;
     romListId: number;
     emulator: Emulator;
     emulatorNameOrg: string;
     romListRoms: RomListRoms[];
 }
 
 
 export class RomDto  {
     romId: number;
     name: string;
     title: string;
     cloneOf: string;
     year: string;
     manufacturer: string;
     category: string;
     players: string;
     rotation: string;
     control: string;
     status: string;
     displayCount: string;
     displayType: string;
     altRomname: string;
     altTitle: string;
     extra: string;
     buttons: string;
     description: string;
     modifiedDate: Date;
     emulatorId: number;
     romListId: number;
     emulator: Emulator;
     emulatorNameOrg: string;
     romListRoms: RomListRoms[];
     haveRomFile: boolean;
     haveBoxartFile: boolean;
     haveCartartFile: boolean;
     haveFanartFile: boolean;
     haveFlyerFile: boolean;
     haveMarqueeFile: boolean;
     haveSnapFile: boolean;
     haveWheelFile: boolean;
 }
 
 
 export class RomList  {
     romListId: number;
     parentRomListId: number;
     parentRomList: RomList;
     title: string;
     subRomLists: RomList[];
     romListRoms: RomListRoms[];
     clientId: number;
     client: Client;
 }
 
 
 export class RomListRoms  {
     romListId: number;
     romList: RomList;
     romId: number;
     rom: Rom;
 }
 
 
 export class System  {
     systemId: number;
     name: string;
     romFolder: string;
     releaseDate: string;
     developer: string;
     manufacturer: string;
     controllers: string;
     cpu: string;
     memory: string;
     graphics: string;
     sound: string;
     display: string;
     media: string;
     description: string;
     fileExtensions: string;
     wiki: string;
     emulators: Emulator[];
 }
 
 
 export class TreeViewNodeDto  {
     id: number;
     name: string;
     hasChildren: boolean;
 }
 
 