using System;
using System.Net.Sockets;

namespace Library.TheraHealth.Models
{
    public enum Gender
    {
        MALE,
        FEMALE,
    };

    public enum Race
    {
        White,
        AfricanAmerican,
        Asian,
        Hispanic,
        AmericanIndian,
        MiddleEasternorNothernAfrican,
        NativeHawaiian,
        Other,
    };

    public class Patient
    {
        internal string name { get; set; }
        internal string address { get; set; }
        internal string notes { get; set; }
        internal Gender gender { get; set; }
        internal DateOnly bDay { get; set; }

        internal Race race { get; set; }

        public Patient(
            string name = "Unknown",
            string address = "N/A",
            string notes = "",
            Race race = Race.Other,
            Gender gender = Gender.MALE,
            DateOnly bDay = default
        )
        {
            this.name = name;
            this.address = address;
            this.race = race;
            this.gender = gender;
            this.notes = notes;
            this.bDay = bDay;
        }


        public override string ToString()
        {
            return $"{name} ({gender}, {race}) - DOB: {bDay:MM/dd/yyyy}";
        }
    }
}
