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
        public int NotificationID { get; set; }
        // [ForeignKey("MessageID")]
        [DisplayName("Message ID")]
        public int MessageID { get; set; }
        [DisplayName("Status")]
        public string Status { get; set; }

        // Might need an endpoint and set of keys
    }
}
