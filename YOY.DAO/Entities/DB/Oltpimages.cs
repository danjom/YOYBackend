using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Oltpimages
    {
        public Oltpimages()
        {
            DeffeaturedSlides = new HashSet<DeffeaturedSlides>();
            DeftenantInformationsEmailBgNavigation = new HashSet<DeftenantInformations>();
            DeftenantInformationsLandingImgNavigation = new HashSet<DeftenantInformations>();
            DeftenantInformationsLogoNavigation = new HashSet<DeftenantInformations>();
            Oltpbtlcontents = new HashSet<Oltpbtlcontents>();
            OltpofferPreferenceOptions = new HashSet<OltpofferPreferenceOptions>();
            OltpoffersCodeImgNavigation = new HashSet<Oltpoffers>();
            OltpoffersDisplayImage = new HashSet<Oltpoffers>();
            OltpproductItems = new HashSet<OltpproductItems>();
            OltpreceiptPictures = new HashSet<OltpreceiptPictures>();
            Oltptransactions = new HashSet<Oltptransactions>();
        }

        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid ReferenceId { get; set; }
        public string Folder { get; set; }
        public string Format { get; set; }
        public string Version { get; set; }
        public string ExternalId { get; set; }
        public string WebTransformation { get; set; }
        public string AppTransformation { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Deftenants Tenant { get; set; }
        public virtual ICollection<DeffeaturedSlides> DeffeaturedSlides { get; set; }
        public virtual ICollection<DeftenantInformations> DeftenantInformationsEmailBgNavigation { get; set; }
        public virtual ICollection<DeftenantInformations> DeftenantInformationsLandingImgNavigation { get; set; }
        public virtual ICollection<DeftenantInformations> DeftenantInformationsLogoNavigation { get; set; }
        public virtual ICollection<Oltpbtlcontents> Oltpbtlcontents { get; set; }
        public virtual ICollection<OltpofferPreferenceOptions> OltpofferPreferenceOptions { get; set; }
        public virtual ICollection<Oltpoffers> OltpoffersCodeImgNavigation { get; set; }
        public virtual ICollection<Oltpoffers> OltpoffersDisplayImage { get; set; }
        public virtual ICollection<OltpproductItems> OltpproductItems { get; set; }
        public virtual ICollection<OltpreceiptPictures> OltpreceiptPictures { get; set; }
        public virtual ICollection<Oltptransactions> Oltptransactions { get; set; }
    }
}
