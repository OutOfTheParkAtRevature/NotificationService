using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Notification ID")]
        public Guid NotificationID { get; set; }
        // This will be like "message", "equipment", etc.
        [DisplayName("Service Requested")]
        public string Service {get;set;}
        // This will be like "added", "edited", etc.
        [DisplayName("Transaction Type")]
        public string TransactionType { get; set; }
    }
}
