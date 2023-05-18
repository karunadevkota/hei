using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace TAskHEI.Models
{
    [Table("tbl_Customer")]  //  explicitly define the table name 
    // naming the table in the database 

    public class Customer
    {
        [Key] // treated as  the primary key in tbl_Customer
        public int CustomerID { get; set; } // unique for each customer and is auto generated 
        [Required] // the value must be non null  that is does not accept any empty data 
        public string Name { get; set; }

        [Required]
        [EmailAddress] //valiadtes the input 
        public string Email { get; set; } //customer's email address

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } //customer's phone number
        [Required]
        public string Address { get; set; }  // customers address

        // the value must be given for every required field
    }
}
