﻿using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpcategoryRelationsView
    {
        public Guid Id { get; set; }
        public Guid ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public int HerarchyLevel { get; set; }
        public Guid? PreferenceId { get; set; }
        public Guid? GeneratorRelationId { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public int? CategoryRelevanceStatus { get; set; }
    }
}
