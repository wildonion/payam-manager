
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;



namespace Payam.Models{

    [Index(nameof(Brand), IsUnique = true)]
    public class DeviceAction
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Brand { get; set; }
        public string Schema { get; set; }
        
        [Required]
        [Column(TypeName = "jsonb")]
        public JsonDocument Actions { get; set; }
    }


    public class DeviceActionBody
    {
        public string Brand { get; set; }
        public string Schema { get; set; }
        // public string Actions { get; set; } // JSON string
        public JsonDocument Actions { get; set; } // JSON object
    }

}

