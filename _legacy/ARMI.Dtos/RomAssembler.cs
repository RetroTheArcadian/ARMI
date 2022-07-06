using ARMI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARMI.Dtos
{
    public static partial class RomAssembler
    {
        static partial void OnDto(this Rom entity, RomDto dto);
        static partial void OnEntity(this RomDto dto, Rom entity);

        public static Rom ToEntity(this RomDto dto)
        {
            if (dto == null) return null;

            var entity = new Rom
            {
                RomId = dto.RomId,
                Name = dto.Name,
                Title = dto.Title,
                CloneOf = dto.CloneOf,
                Year = dto.Year,
                Manufacturer = dto.Manufacturer,
                Category = dto.Category,
                Players = dto.Players,
                Rotation = dto.Rotation,
                Control = dto.Control,
                Status = dto.Status,
                DisplayCount = dto.DisplayCount,
                DisplayType = dto.DisplayType,
                AltRomname = dto.AltRomname,
                AltTitle = dto.AltTitle,
                Extra = dto.Extra,
                Buttons = dto.Buttons,
                Description = dto.Description,
                ModifiedDate = dto.ModifiedDate,

                EmulatorId = dto.EmulatorId,
                RomListId = dto.RomListId,
                Emulator = dto.Emulator,
                EmulatorNameOrg = dto.EmulatorNameOrg,
                RomListRoms = dto.RomListRoms
            };
            dto.OnEntity(entity);
            return entity;
        }

        public static RomDto ToDto(this Rom entity, bool includeFileInformation = false)
        {
            if (entity == null) return null;

            var dto = new RomDto
            {
                RomId = entity.RomId,
                Name = entity.Name,
                Title = entity.Title,
                CloneOf = entity.CloneOf,
                Year = entity.Year,
                Manufacturer = entity.Manufacturer,
                Category = entity.Category,
                Players = entity.Players,
                Rotation = entity.Rotation,
                Control = entity.Control,
                Status = entity.Status,
                DisplayCount = entity.DisplayCount,
                DisplayType = entity.DisplayType,
                AltRomname = entity.AltRomname,
                AltTitle = entity.AltTitle,
                Extra = entity.Extra,
                Buttons = entity.Buttons,
                Description = entity.Description,
                ModifiedDate = entity.ModifiedDate,

                EmulatorId = entity.EmulatorId,
                RomListId = entity.RomListId,
                Emulator = entity.Emulator,
                EmulatorNameOrg = entity.EmulatorNameOrg,
                RomListRoms = entity.RomListRoms.ToList()
            };
            entity.OnDto(dto);
            return dto;
        }

        public static List<Rom> ToEntities(this IEnumerable<RomDto> dtos)
        {
            return dtos?.Select(e => e.ToEntity()).ToList();
        }

        public static List<RomDto> ToDtos(this IEnumerable<Rom> entities)
        {
            return entities?.Select(e => e.ToDto()).ToList();
        }
    }
}
