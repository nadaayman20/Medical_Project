using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment.Models
{
    public enum Gender
    {
        Female,
        Male
    }

    public enum Spectialist
    {
        Dentists,
        Neurology,
        Ophthalmology,
        Orthopedics,
        Cancer_Department,
        Internal_medicine,
        ENT

    }

    public enum MedicalDegree
    {
        specialist,

        Advisor,

        professor
    }
    public enum AvalibalDays
    {
        Saturday, Sunday, Monday, Tuesday, Wednesday, Thursday
    }
    public class Doctor
    {
            [Key]
            public int DoctorID { get; set; }

            [Required]
            [MinLength(10)]
            public String FullName { get; set; }

            [Required]
            [EnumDataType(typeof(Gender))]
            public Gender gender { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Required]
            [EnumDataType(typeof(Spectialist))]
            public Spectialist specialist { get; set; }

            [Required]
            [EnumDataType(typeof(MedicalDegree))]
            public MedicalDegree Degree { get; set; }

            public string Description { get; set; }

            [Required]
            public string Clinic_Location { get; set; }

            [Required]
            [DataType(DataType.PhoneNumber)]
            public int Clinic_PhonNum { get; set; }

            [Required]
            [Range(1, 24)]
            public int ReservationStartTime { get; set; }

            [Required]
            [Range(1, 24)]
            public int ReservationEndTime { get; set; }

            [Required]
            [EnumDataType(typeof(AvalibalDays))]
            public AvalibalDays WorkDays { get; set; }

            //[DataType(DataType.ImageUrl)]
            //public byte[] Image { get; set; }

            public bool HomeExamination { get; set; }

        }

    
}
