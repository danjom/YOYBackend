using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Oltpcategories
    {
        public Oltpcategories()
        {
            DefdepartmentCategories = new HashSet<DefdepartmentCategories>();
            Defkeywords = new HashSet<Defkeywords>();
            DeftenantInformations = new HashSet<DeftenantInformations>();
            InverseParentCategoryNavigation = new HashSet<Oltpcategories>();
            Oltpbtlcontents = new HashSet<Oltpbtlcontents>();
            OltpcategoryRelations = new HashSet<OltpcategoryRelations>();
            Oltpoffers = new HashSet<Oltpoffers>();
            OltpproductItems = new HashSet<OltpproductItems>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? RelevanceStatus { get; set; }
        public int PurposeType { get; set; }
        public bool? IsActive { get; set; }
        public bool IsSystemCategory { get; set; }
        public Guid? ParentCategory { get; set; }
        public string Icon { get; set; }
        public string CarrouselImg { get; set; }
        public int? HerarchyLevel { get; set; }

        public virtual Oltpcategories ParentCategoryNavigation { get; set; }
        public virtual ICollection<DefdepartmentCategories> DefdepartmentCategories { get; set; }
        public virtual ICollection<Defkeywords> Defkeywords { get; set; }
        public virtual ICollection<DeftenantInformations> DeftenantInformations { get; set; }
        public virtual ICollection<Oltpcategories> InverseParentCategoryNavigation { get; set; }
        public virtual ICollection<Oltpbtlcontents> Oltpbtlcontents { get; set; }
        public virtual ICollection<OltpcategoryRelations> OltpcategoryRelations { get; set; }
        public virtual ICollection<Oltpoffers> Oltpoffers { get; set; }
        public virtual ICollection<OltpproductItems> OltpproductItems { get; set; }
    }
}
