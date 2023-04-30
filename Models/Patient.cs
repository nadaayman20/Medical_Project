using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment.Models
{
    public enum PGender
    {
        Female,
        Male
    }
    public class Patient
    {
            [Key]
            public int PatientID { get; set; }

            [Required]
            [MinLength(10)]
            public string FullName { get; set; }

            [Required]
            [EnumDataType(typeof(PGender))]
            public PGender gender { get; set; }

            [Required]
            public int Age { get; set; }

            [Required]
            [DataType(DataType.PhoneNumber)]
            public int PhonNum { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            public string Address { get; set; }


        }
    }

