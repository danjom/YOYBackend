using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.Values;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class MembershipLevelManager
    {
        #region PROPERTIES_AND_RESOURCES

        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS PRIVATE PROPERTIES AND RESOURCES                                                                                                         //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        // PARENT BUSINESS OBJECTS ---------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Parent business objects 
        /// </summary>
        private readonly BusinessObjects _businessObjects;

        #endregion

        #region MEMBERSHIPLEVELS

        public List<MembershipLevel> Gets()
        {
            List<MembershipLevel> levels = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefmembershipLevels
                            orderby x.Level ascending
                            select x;

                if (query != null)
                {
                    MembershipLevel level = null;
                    levels = new List<MembershipLevel>();

                    foreach (DefmembershipLevels item in query)
                    {
                        level = new MembershipLevel
                        {
                            Id = item.Id,
                            Level = item.Level,
                            IconUrl = item.IconUrl,
                            Name = item.Name,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };
                        levels.Add(level);
                    }
                }
            }
            catch (Exception e)
            {
                levels = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return levels;
        }

        public MembershipLevel Get(Guid id)
        {
            MembershipLevel level = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefmembershipLevels
                            where x.Id == id
                            select x;

                if (query != null)
                {

                    foreach (DefmembershipLevels item in query)
                    {
                        level = new MembershipLevel
                        {
                            Id = item.Id,
                            Level = item.Level,
                            IconUrl = item.IconUrl,
                            Name = item.Name,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                level = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return level;
        }

        public MembershipLevel Post(string name, int levelPos, string iconUrl)
        {
            MembershipLevel level;
            try
            {
                DefmembershipLevels newLevel = new DefmembershipLevels
                {
                    Id = Guid.NewGuid(),
                    Level = levelPos,
                    IconUrl = iconUrl,
                    Name = name,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow

                };

                this._businessObjects.Context.DefmembershipLevels.Add(newLevel);
                this._businessObjects.Context.SaveChanges();

                level = new MembershipLevel
                {
                    Id = newLevel.Id,
                    Level = newLevel.Level,
                    IconUrl = newLevel.IconUrl,
                    Name = newLevel.Name,
                    CreatedDate = newLevel.CreatedDate,
                    UpdatedDate = newLevel.UpdatedDate
                };
            }
            catch (Exception e)
            {
                level = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return level;
        }

        public MembershipLevel Put(Guid id, string name, string iconUrl)
        {
            MembershipLevel level = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefmembershipLevels
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    DefmembershipLevels currentLevel = null;

                    foreach (DefmembershipLevels item in query)
                    {
                        currentLevel = item;
                    }

                    if (currentLevel != null)
                    {
                        currentLevel.Name = name;
                        currentLevel.IconUrl = iconUrl;
                        currentLevel.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        level = new MembershipLevel
                        {
                            Id = currentLevel.Id,
                            Level = currentLevel.Level,
                            IconUrl = currentLevel.IconUrl,
                            Name = currentLevel.Name,
                            CreatedDate = currentLevel.CreatedDate,
                            UpdatedDate = currentLevel.UpdatedDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                level = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return level;
        }

        public bool Delete(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.DefmembershipLevels
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    DefmembershipLevels currentLevel = null;

                    foreach (DefmembershipLevels item in query)
                    {
                        currentLevel = item;
                    }

                    this._businessObjects.Context.DefmembershipLevels.Remove(currentLevel);
                    this._businessObjects.Context.SaveChanges();

                    success = true;
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        #endregion

        #region TENANTMEMBERSHIPLEVELS

        public List<TenantMembershipLevel> Gets(Guid tenantId)
        {
            List<TenantMembershipLevel> tenantLevels = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefmembershipLevelsView
                            where x.TenantId == tenantId
                            orderby x.LevelPos ascending
                            select x;

                if (query != null)
                {
                    TenantMembershipLevel tenantLevel = null;
                    tenantLevels = new List<TenantMembershipLevel>();

                    foreach (DefmembershipLevelsView item in query)
                    {
                        tenantLevel = new TenantMembershipLevel
                        {
                            TenantId = item.TenantId,
                            TenantName = item.TenantName,
                            TenantLogo = item.TenantLogo,
                            TenantCategoryId = item.TenantCategoryId,
                            AcceptCommunityPointsAsPayment = item.AcceptsCommunityPointsAsPayment,
                            AcceptsSelfPointsAsPayment = item.AcceptsSelfPointsAsPayment,
                            CurrencySymbol = item.CurrencySymbol,
                            HasMembershipLevels = item.HasMembershipLevels,
                            LoyaltyProgramType = item.LoyaltyProgramType,
                            LevelId = item.LevelId,
                            LevelName = item.LevelName,
                            LevelPos = item.LevelPos,
                            EnabledActions = item.EnabledActions,
                            MaxGeneratedPoints = item.MaxGeneratedPoints,
                            MinGeneratedPoints = item.MinGeneratedPoints,
                            MinPurchasesCount = item.MinPurchasesCount,
                            MaxPurchasesCount = item.MaxPurchasesCount,
                            MaxRewardRedemptions = item.MaxRewardRedemptions,
                            EvaluationMonths = item.EvaluationMonths,
                            LoyaltyCashBackPercentage = item.LoyaltyCashBackPercentage,
                            PointsLifeSpanMonths = item.PointsLifeSpanMonths,
                            CheckInPoints = item.CheckInPoints,
                            MonetaryConversionFactor = item.MonetaryConversionFactor,
                            IconUrl = item.IconUrl,
                            IsActive = item.IsActive,
                            PointsToMoneyEnabled = item.PointsToMoneyEnabled ?? false,
                            EnabledMoneyAmounts = item.EnabledMoneyAmounts,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        tenantLevels.Add(tenantLevel);
                    }
                }
            }
            catch (Exception e)
            {
                tenantLevels = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tenantLevels;
        }

        public TenantMembershipLevel Get(Guid tenantId, Guid levelId)
        {
            TenantMembershipLevel tenantLevel = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefmembershipLevelsView
                            where x.TenantId == tenantId && x.LevelId == levelId
                            select x;

                if (query != null)
                {

                    foreach (DefmembershipLevelsView item in query)
                    {
                        tenantLevel = new TenantMembershipLevel
                        {
                            TenantId = item.TenantId,
                            TenantName = item.TenantName,
                            TenantLogo = item.TenantLogo,
                            TenantCategoryId = item.TenantCategoryId,
                            AcceptCommunityPointsAsPayment = item.AcceptsCommunityPointsAsPayment,
                            AcceptsSelfPointsAsPayment = item.AcceptsSelfPointsAsPayment,
                            CurrencySymbol = item.CurrencySymbol,
                            HasMembershipLevels = item.HasMembershipLevels,
                            LoyaltyProgramType = item.LoyaltyProgramType,
                            LevelId = item.LevelId,
                            LevelName = item.LevelName,
                            LevelPos = item.LevelPos,
                            EnabledActions = item.EnabledActions,
                            MaxGeneratedPoints = item.MaxGeneratedPoints,
                            MinGeneratedPoints = item.MinGeneratedPoints,
                            MinPurchasesCount = item.MinPurchasesCount,
                            MaxPurchasesCount = item.MaxPurchasesCount,
                            MaxRewardRedemptions = item.MaxRewardRedemptions,
                            EvaluationMonths = item.EvaluationMonths,
                            LoyaltyCashBackPercentage = item.LoyaltyCashBackPercentage,
                            PointsLifeSpanMonths = item.PointsLifeSpanMonths,
                            CheckInPoints = item.CheckInPoints,
                            MonetaryConversionFactor = item.MonetaryConversionFactor,
                            IconUrl = item.IconUrl,
                            IsActive = item.IsActive,
                            PointsToMoneyEnabled = item.PointsToMoneyEnabled ?? false,
                            EnabledMoneyAmounts = item.EnabledMoneyAmounts,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                tenantLevel = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tenantLevel;
        }

        public TenantMembershipLevel Get(Guid tenantId, int levelPos)
        {
            TenantMembershipLevel tenantLevel = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefmembershipLevelsView
                            where x.TenantId == tenantId && x.LevelPos == levelPos
                            select x;

                if (query != null)
                {

                    foreach (DefmembershipLevelsView item in query)
                    {
                        tenantLevel = new TenantMembershipLevel
                        {
                            TenantId = item.TenantId,
                            TenantName = item.TenantName,
                            TenantLogo = item.TenantLogo,
                            TenantCategoryId = item.TenantCategoryId,
                            AcceptCommunityPointsAsPayment = item.AcceptsCommunityPointsAsPayment,
                            AcceptsSelfPointsAsPayment = item.AcceptsSelfPointsAsPayment,
                            CurrencySymbol = item.CurrencySymbol,
                            HasMembershipLevels = item.HasMembershipLevels,
                            LoyaltyProgramType = item.LoyaltyProgramType,
                            LevelId = item.LevelId,
                            LevelName = item.LevelName,
                            LevelPos = item.LevelPos,
                            EnabledActions = item.EnabledActions,
                            MaxGeneratedPoints = item.MaxGeneratedPoints,
                            MinGeneratedPoints = item.MinGeneratedPoints,
                            MinPurchasesCount = item.MinPurchasesCount,
                            MaxPurchasesCount = item.MaxPurchasesCount,
                            MaxRewardRedemptions = item.MaxRewardRedemptions,
                            EvaluationMonths = item.EvaluationMonths,
                            LoyaltyCashBackPercentage = item.LoyaltyCashBackPercentage,
                            PointsLifeSpanMonths = item.PointsLifeSpanMonths,
                            CheckInPoints = item.CheckInPoints,
                            MonetaryConversionFactor = item.MonetaryConversionFactor,
                            IconUrl = item.IconUrl,
                            IsActive = item.IsActive,
                            PointsToMoneyEnabled = item.PointsToMoneyEnabled ?? false,
                            EnabledMoneyAmounts = item.EnabledMoneyAmounts,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                tenantLevel = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tenantLevel;
        }

        public TenantMembershipLevel Post(Guid tenantId, Guid levelId, double loyaltyCashBackPercentage, int minGeneratedPoints, int maxGeneratedPoints, int minPurchasesCount, int maxPurchasesCount, int maxRewardRedemptions,
            int evaluationMonths, int pointsLifeSpanMonths, double monetaryConversionFactor, int checkInPoints, string enabledActions, bool pointsToMoneyEnabled, string enabledMoneyAmounts)
        {
            TenantMembershipLevel tenantLevel = null;

            try
            {
                DeftenantMembershipLevels newTenantLevel = new DeftenantMembershipLevels
                {
                    TenantId = tenantId,
                    LevelId = levelId,
                    IsActive = true,
                    LoyaltyCashBackPercentage = loyaltyCashBackPercentage,
                    EvaluationMonths = evaluationMonths,
                    MinGeneratedPoints = minGeneratedPoints,
                    MaxGeneratedPoints = maxGeneratedPoints,
                    MinPurchasesCount = minPurchasesCount,
                    MaxPurchasesCount = maxPurchasesCount,
                    MaxRewardRedemptions = maxRewardRedemptions,
                    PointsLifeSpanMonths = pointsLifeSpanMonths,
                    CheckInPoints = checkInPoints,
                    MonetaryConversionFactor = monetaryConversionFactor,
                    EnabledActions = enabledActions,
                    PointsToMoneyEnabled = pointsToMoneyEnabled,
                    EnabledMoneyAmounts = enabledMoneyAmounts,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.DeftenantMembershipLevels.Add(newTenantLevel);
                this._businessObjects.Context.SaveChanges();

                var query = from x in this._businessObjects.Context.DefmembershipLevelsView
                            where x.TenantId == newTenantLevel.TenantId && x.LevelId == newTenantLevel.LevelId
                            select x;

                if (query != null)
                {

                    foreach (DefmembershipLevelsView item in query)
                    {
                        tenantLevel = new TenantMembershipLevel
                        {
                            TenantId = item.TenantId,
                            TenantName = item.TenantName,
                            TenantLogo = item.TenantLogo,
                            TenantCategoryId = item.TenantCategoryId,
                            AcceptCommunityPointsAsPayment = item.AcceptsCommunityPointsAsPayment,
                            AcceptsSelfPointsAsPayment = item.AcceptsSelfPointsAsPayment,
                            CurrencySymbol = item.CurrencySymbol,
                            HasMembershipLevels = item.HasMembershipLevels,
                            LoyaltyProgramType = item.LoyaltyProgramType,
                            LevelId = item.LevelId,
                            LevelName = item.LevelName,
                            LevelPos = item.LevelPos,
                            EnabledActions = item.EnabledActions,
                            MaxGeneratedPoints = item.MaxGeneratedPoints,
                            MinGeneratedPoints = item.MinGeneratedPoints,
                            MinPurchasesCount = item.MinPurchasesCount,
                            MaxPurchasesCount = item.MaxPurchasesCount,
                            MaxRewardRedemptions = item.MaxRewardRedemptions,
                            EvaluationMonths = item.EvaluationMonths,
                            LoyaltyCashBackPercentage = item.LoyaltyCashBackPercentage,
                            PointsLifeSpanMonths = item.PointsLifeSpanMonths,
                            CheckInPoints = item.CheckInPoints,
                            MonetaryConversionFactor = item.MonetaryConversionFactor,
                            IconUrl = item.IconUrl,
                            IsActive = item.IsActive,
                            PointsToMoneyEnabled = item.PointsToMoneyEnabled ?? false,
                            EnabledMoneyAmounts = item.EnabledMoneyAmounts,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                tenantLevel = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tenantLevel;
        }

        public TenantMembershipLevel Put(Guid tenantId, Guid levelId, double loyaltyCashBackPercentage, int minGeneratedPoints, int maxGeneratedPoints, int minPurchasesCount, int maxPurchasesCount, int maxRewardRedemptions,
            int evaluationMonths, int pointsLifeSpanMonths, string enabledActions, bool pointsToMoneyEnabled, string enabledMoneyAmounts)
        {
            TenantMembershipLevel tenantLevel = null;

            try
            {
                var query = from x in this._businessObjects.Context.DeftenantMembershipLevels
                            where x.TenantId == tenantId && x.LevelId == levelId
                            select x;

                if (query != null)
                {
                    DeftenantMembershipLevels currentTenantLevel = null;

                    foreach (DeftenantMembershipLevels item in query)
                    {
                        currentTenantLevel = item;
                    }

                    if (currentTenantLevel != null)
                    {
                        currentTenantLevel.LoyaltyCashBackPercentage = loyaltyCashBackPercentage;
                        currentTenantLevel.MinGeneratedPoints = minGeneratedPoints;
                        currentTenantLevel.MaxGeneratedPoints = maxGeneratedPoints;
                        currentTenantLevel.MinPurchasesCount = minPurchasesCount;
                        currentTenantLevel.MaxPurchasesCount = maxPurchasesCount;
                        currentTenantLevel.MaxRewardRedemptions = maxRewardRedemptions;
                        currentTenantLevel.EvaluationMonths = evaluationMonths;
                        currentTenantLevel.PointsLifeSpanMonths = pointsLifeSpanMonths;
                        currentTenantLevel.EnabledActions = enabledActions;
                        currentTenantLevel.PointsToMoneyEnabled = pointsToMoneyEnabled;
                        currentTenantLevel.EnabledMoneyAmounts = enabledMoneyAmounts;
                        currentTenantLevel.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        var queryView = from x in this._businessObjects.Context.DefmembershipLevelsView
                                        where x.TenantId == currentTenantLevel.TenantId && x.LevelId == currentTenantLevel.LevelId
                                        select x;

                        if (query != null)
                        {

                            foreach (DefmembershipLevelsView item in queryView)
                            {
                                tenantLevel = new TenantMembershipLevel
                                {
                                    TenantId = item.TenantId,
                                    TenantName = item.TenantName,
                                    TenantLogo = item.TenantLogo,
                                    TenantCategoryId = item.TenantCategoryId,
                                    AcceptCommunityPointsAsPayment = item.AcceptsCommunityPointsAsPayment,
                                    AcceptsSelfPointsAsPayment = item.AcceptsSelfPointsAsPayment,
                                    CurrencySymbol = item.CurrencySymbol,
                                    HasMembershipLevels = item.HasMembershipLevels,
                                    LoyaltyProgramType = item.LoyaltyProgramType,
                                    LevelId = item.LevelId,
                                    LevelName = item.LevelName,
                                    LevelPos = item.LevelPos,
                                    EnabledActions = item.EnabledActions,
                                    MaxGeneratedPoints = item.MaxGeneratedPoints,
                                    MinGeneratedPoints = item.MinGeneratedPoints,
                                    MinPurchasesCount = item.MinPurchasesCount,
                                    MaxPurchasesCount = item.MaxPurchasesCount,
                                    EvaluationMonths = item.EvaluationMonths,
                                    LoyaltyCashBackPercentage = item.LoyaltyCashBackPercentage,
                                    PointsLifeSpanMonths = item.PointsLifeSpanMonths,
                                    CheckInPoints = item.CheckInPoints,
                                    MonetaryConversionFactor = item.MonetaryConversionFactor,
                                    IconUrl = item.IconUrl,
                                    IsActive = item.IsActive,
                                    PointsToMoneyEnabled = item.PointsToMoneyEnabled ?? false,
                                    EnabledMoneyAmounts = item.EnabledMoneyAmounts,
                                    CreatedDate = item.CreatedDate,
                                    UpdatedDate = item.UpdatedDate
                                };
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                tenantLevel = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return tenantLevel;
        }

        public bool Put(Guid tenantId, Guid levelId)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.DeftenantMembershipLevels
                            where x.TenantId == tenantId && x.LevelId == levelId
                            select x;

                if (query != null)
                {
                    DeftenantMembershipLevels currentTenantLevel = null;

                    foreach (DeftenantMembershipLevels item in query)
                    {
                        currentTenantLevel = item;
                    }

                    if (currentTenantLevel != null)
                    {
                        currentTenantLevel.IsActive = !currentTenantLevel.IsActive;
                        currentTenantLevel.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        public bool Delete(Guid tenantId, Guid levelId)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.DeftenantMembershipLevels
                            where x.TenantId == tenantId && x.LevelId == levelId
                            select x;

                if (query != null)
                {
                    DeftenantMembershipLevels currentTenantLevel = null;

                    foreach (DeftenantMembershipLevels item in query)
                    {
                        currentTenantLevel = item;
                    }

                    if (currentTenantLevel != null)
                    {
                        this._businessObjects.Context.DeftenantMembershipLevels.Remove(currentTenantLevel);
                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public MembershipLevelManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD PRODUCT PREFERENCE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
