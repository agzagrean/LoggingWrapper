using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iris.Logging.Dal.Entities
{
    [Table("IntegrationLogging")]
    public abstract class IntegrationLogEntity: BaseEntityObject
    {
        private const string Index = "PK_IntegrationLogging";

        [Index(Index, Order = 1)]
        [StringLength(10)]
        public string LogLevel { get; set; }

        [Index(Index, Order = 2)]
        [StringLength(10)]
        public string MessageDirection { get; set; }

        [Index(Index, Order = 3)]
        [StringLength(20)]
        public string ThirdPartySystemId { get; set; }

        [Index(Index, Order = 4)]
        [StringLength(20)]
        public string ThirdPartySystemType { get; set; }

        [Index(Index, Order = 5)]
        [StringLength(50)]
        public string PropertyCode { get; set; }

        [Index(Index, Order = 6)]
        public DateTime CreatedDateUtc { get; set; }

        [Index(Index, Order = 7)]
        [StringLength(255)]
        public string Component { get; set; }

        [Index(Index, Order = 8)]
        [StringLength(Int32.MaxValue)]
        public string Request { get; set; }

        [Index(Index, Order =9)]
        [StringLength(Int32.MaxValue)]
        public string Response { get; set; }

        [Index(Index, Order = 10)]
        [StringLength(Int32.MaxValue)]
        public string Message { get; set; }

        [Index(Index, Order = 11)]
        [StringLength(Int32.MaxValue)]
        public string Exception { get; set; }
    }
}
