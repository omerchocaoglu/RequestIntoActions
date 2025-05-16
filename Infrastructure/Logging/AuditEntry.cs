using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.DTOModels;
using Domain.EntitiyModels.AuditLogModels;
using Domain.EntitiyModels.UserModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Logging
{
    public class AuditEntry
    {
        public AuditEntry( EntityEntry entry ) // Amac?
        {
            Entry = entry;
        }
        public EntityEntry Entry { get; } // Bu neden böyle var?
        public string TableName  { get; set; }
        public string OperationType { get; set; }
        public Dictionary<string, object> OldValues { get; } = new();
        public Dictionary<string, object> NewValues { get; } = new(); // bunun amacı ne?
        public string Changes { get; set; }

        public AuditLog ToAuditLog()
        {
            
            return new AuditLog
            {
                TableName = TableName,
                OperationType = OperationType,
                OldValues = OldValues.Count == 0 ? null : JsonSerializer.Serialize(OldValues), // bunun amacı ne?
                NewValues = NewValues.Count == 0 ? null : JsonSerializer.Serialize(NewValues),
                CreateOn = DateTime.Now,
                Changes = Changes,
                RecordID = Entry.Properties.FirstOrDefault( p => p.Metadata.IsPrimaryKey())?.CurrentValue is int id ? id: 0
            };
        }
    }
}
