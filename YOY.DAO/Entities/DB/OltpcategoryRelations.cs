using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpcategoryRelations
    {
        public OltpcategoryRelations()
        {
            InverseGeneratorRelation = new HashSet<OltpcategoryRelations>();
        }

        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public int HerarchyLevel { get; set; }
        public Guid? GeneratorRelationId { get; set; }
        public Guid? PreferenceId { get; set; }
        public Guid ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Oltpcategories Category { get; set; }
        public virtual OltpcategoryRelations GeneratorRelation { get; set; }
        public virtual ICollection<OltpcategoryRelations> InverseGeneratorRelation { get; set; }
    }
}
