using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Doctor_Appointment.Models
{
    public enum AppointmentType
    {
        ClinicalExaminiation,
        HomeExamination
    }

    public enum Days
    {
        Saturday, Sunday, Monday, Tuesday, Wednesday, Thursday
    }
      [PrimaryKey(nameof(DoctorID), nameof(PatientID))]
    public class Appointment
    {
           //[Key, Column(Order = 0)]
          [ForeignKey("Doctor")]
            public int DoctorID { get; set; }
            public Doctor doctor { get; set; }

           //[Key, Column(Order = 1)]
           [ForeignKey("Patient")]
            public int PatientID { get; set; }
            public Patient patient { get; set; }

            [EnumDataType(typeof(Days))]
            public Days AppointmentDay { get; set; }

            [Range(1, 24)]
            public int AppointmentTime { get; set; }

            [EnumDataType(typeof(AppointmentType))]
            public AppointmentType AppointmentType { get; set; }

            public string MedicalHistory { get; set; }


        }

    }

