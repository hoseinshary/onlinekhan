using System;
using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Package
    {

        public Package()
        {
            Lessons = new HashSet<Lesson>();
            Assays = new HashSet<Assay>();
        }


        public int Id { get; set; }

        public string  Name { get; set; }

        public bool IsDelete { get; set; }

        public string  ImageFile { get; set; }

        public bool IsActive { get; set; }

        public int Price { get; set; }

        public DateTime Expire { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string Discription { get; set; }

        public ICollection<Lesson> Lessons { get; set; }

        public ICollection<Assay> Assays { get; set; }

        public ICollection<Sale_Package> Sale_Packages { get; set; }


    }
}
