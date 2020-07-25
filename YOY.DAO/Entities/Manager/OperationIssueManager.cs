using YOY.DTO.Entities;
using YOY.Values;
using YOY.Values.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class OperationIssueManager
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

        #region METHODS

        private string GetRefTypeName(int refType)
        {
            string refTypeName = "";

            switch (refType)
            {
                case OperationIssueRefTypes.None:
                    refTypeName = Resources.None;
                    break;
                case OperationIssueRefTypes.Transaction:
                    refTypeName = Resources.Transaction;
                    break;
                case OperationIssueRefTypes.Receipt:
                    refTypeName = Resources.Receipt;
                    break;
            }

            return refTypeName;
        }

        private string GetIssueTypeName(int issueType)
        {
            string issueTypeName = "";

            switch (issueType)
            {
                case OperationIssueTypes.DealDeniel:
                    issueTypeName = Resources.DealDeniel;
                    break;
                case OperationIssueTypes.OutOfStockDeal:
                    issueTypeName = Resources.OutOfStockDeal;
                    break;
                case OperationIssueTypes.CodeNotVisible:
                    issueTypeName = Resources.CodeNotVisible;
                    break;
                case OperationIssueTypes.InvalidCode:
                    issueTypeName = Resources.InvalidCode;
                    break;
                case OperationIssueTypes.CheckTicketManually:
                    issueTypeName = Resources.CheckTicketManually;
                    break;
                case OperationIssueTypes.IncorrectPurchaseAmount:
                    issueTypeName = Resources.IncorrectPurchaseAmount;
                    break;
            }

            return issueTypeName;
        }

        private string GetStatusName(int status)
        {
            string statusName = "";

            switch (status)
            {
                case OperationIssueStatuses.Opened:
                    statusName = Resources.Opened;
                    break;
                case OperationIssueStatuses.Attending:
                    statusName = Resources.Attending;
                    break;
                case OperationIssueStatuses.Resolved:
                    statusName = Resources.Resolved;
                    break;
                case OperationIssueStatuses.Closed:
                    statusName = Resources.Closed;
                    break;
            }

            return statusName;
        }

        public List<OperationIssue> Gets(Guid? tenantId, Guid? branchId, string userId, int refType, int issueType, int status, DateTime minDate, DateTime maxDate, int pageSize, int pageNumber)
        {
            List<OperationIssue> operationIssues = null;

            try
            {
                var query = (dynamic)null;

                if (string.IsNullOrWhiteSpace(userId))
                {
                    if (refType != OperationIssueRefTypes.All)
                    {
                        if (issueType != OperationIssueTypes.All)
                        {
                            if (status != OperationIssueStatuses.All)
                            {
                                if (tenantId != null)
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.BranchId == branchId && x.RefType == refType && x.IssueType == issueType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.RefType == refType && x.IssueType == issueType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.BranchId == branchId && x.RefType == refType && x.IssueType == issueType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.RefType == refType && x.IssueType == issueType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (tenantId != null)
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.BranchId == branchId && x.RefType == refType && x.IssueType == issueType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.RefType == refType && x.IssueType == issueType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.BranchId == branchId && x.RefType == refType && x.IssueType == issueType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.RefType == refType && x.IssueType == issueType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (status != OperationIssueStatuses.All)
                            {
                                if (tenantId != null)
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.BranchId == branchId && x.RefType == refType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.RefType == refType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.BranchId == branchId && x.RefType == refType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.RefType == refType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (tenantId != null)
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.BranchId == branchId && x.RefType == refType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.RefType == refType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.BranchId == branchId && x.RefType == refType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.RefType == refType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (issueType != OperationIssueTypes.All)
                        {
                            if (status != OperationIssueStatuses.All)
                            {
                                if (tenantId != null)
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.BranchId == branchId && x.IssueType == issueType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.IssueType == issueType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.BranchId == branchId && x.IssueType == issueType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.IssueType == issueType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (tenantId != null)
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.BranchId == branchId && x.IssueType == issueType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.IssueType == issueType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.BranchId == branchId && x.IssueType == issueType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.IssueType == issueType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (status != OperationIssueStatuses.All)
                            {
                                if (tenantId != null)
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.BranchId == branchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (tenantId != null)
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.BranchId == branchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.BranchId == branchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.UserId == userId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //No specific user
                    if (refType != OperationIssueRefTypes.All)
                    {
                        if (issueType != OperationIssueTypes.All)
                        {
                            if (status != OperationIssueStatuses.All)
                            {
                                if (tenantId != null)
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.TenantId == tenantId && x.BranchId == branchId && x.RefType == refType && x.IssueType == issueType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.TenantId == tenantId && x.RefType == refType && x.IssueType == issueType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.BranchId == branchId && x.RefType == refType && x.IssueType == issueType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.RefType == refType && x.IssueType == issueType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (tenantId != null)
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.TenantId == tenantId && x.BranchId == branchId && x.RefType == refType && x.IssueType == issueType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.TenantId == tenantId && x.RefType == refType && x.IssueType == issueType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.BranchId == branchId && x.RefType == refType && x.IssueType == issueType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.RefType == refType && x.IssueType == issueType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (status != OperationIssueStatuses.All)
                            {
                                if (tenantId != null)
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.TenantId == tenantId && x.BranchId == branchId && x.RefType == refType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.TenantId == tenantId && x.RefType == refType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.BranchId == branchId && x.RefType == refType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.RefType == refType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (tenantId != null)
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.TenantId == tenantId && x.BranchId == branchId && x.RefType == refType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.TenantId == tenantId && x.RefType == refType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.BranchId == branchId && x.RefType == refType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.RefType == refType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (issueType != OperationIssueTypes.All)
                        {
                            if (status != OperationIssueStatuses.All)
                            {
                                if (tenantId != null)
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.TenantId == tenantId && x.BranchId == branchId && x.IssueType == issueType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.TenantId == tenantId && x.IssueType == issueType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.BranchId == branchId && x.IssueType == issueType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.IssueType == issueType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (tenantId != null)
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.TenantId == tenantId && x.BranchId == branchId && x.IssueType == issueType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.TenantId == tenantId && x.IssueType == issueType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.BranchId == branchId && x.IssueType == issueType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.IssueType == issueType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (status != OperationIssueStatuses.All)
                            {
                                if (tenantId != null)
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.TenantId == tenantId && x.BranchId == branchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.TenantId == tenantId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.TenantId == tenantId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (tenantId != null)
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.TenantId == tenantId && x.BranchId == branchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.TenantId == tenantId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (branchId != null)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.BranchId == branchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                                                 where x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                    }
                }

                if (query != null)
                {
                    OperationIssue operationIssue = null;
                    operationIssues = new List<OperationIssue>();

                    foreach (OltpoperationIssuesView item in query)
                    {
                        operationIssue = new OperationIssue
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            TenantId = item.TenantId,
                            BranchId = item.BranchId,
                            RefId = item.RefId,
                            RefType = item.RefType,
                            RefTypeName = GetRefTypeName(item.RefType),
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.LastUpdate,
                            Details = item.Details,
                            Comments = item.Comments,
                            ContactInfo = item.ContactInfo,
                            IssueType = item.IssueType,
                            IssueTypeName = GetIssueTypeName(item.IssueType),
                            Status = item.Status,
                            StatusName = GetStatusName(item.Status),
                            LastUpdate = item.LastUpdate,
                            TenantName = item.TenantName,
                            TenantLogo = item.TenantLogo,
                            TenantContactEmail = item.TenantContactEmail,
                            TenantContactPhone = item.TenantContactPhone,
                            TenantContactName = item.TenantContactName,
                            UserName = item.UserName,
                            UserEmail = item.UserEmail,
                            UserAccountNumber = item.UserAccountNumber
                        };

                        operationIssues.Add(operationIssue);
                    }
                }

            }
            catch (Exception e)
            {
                operationIssues = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return operationIssues;
        }

        public List<OperationIssue> Gets(string userId, Guid tenantId, Guid referenceId, int referenceType, int pageSize, int pageNumber)
        {
            List<OperationIssue> operationIssues = null;

            try
            {
                var query = (from x in this._businessObjects.Context.OltpoperationIssuesView
                             where x.UserId == userId && x.TenantId == tenantId && x.RefType == referenceType && x.RefId == referenceId
                             orderby x.CreatedDate descending
                             select x).Skip(pageSize * pageNumber).Take(pageSize);

                if (query != null)
                {
                    OperationIssue operationIssue = null;
                    operationIssues = new List<OperationIssue>();

                    foreach (OltpoperationIssuesView item in query)
                    {
                        operationIssue = new OperationIssue
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            TenantId = item.TenantId,
                            BranchId = item.BranchId,
                            RefId = item.RefId,
                            RefType = item.RefType,
                            RefTypeName = GetRefTypeName(item.RefType),
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.LastUpdate,
                            Details = item.Details,
                            Comments = item.Comments,
                            ContactInfo = item.ContactInfo,
                            IssueType = item.IssueType,
                            IssueTypeName = GetIssueTypeName(item.IssueType),
                            Status = item.Status,
                            StatusName = GetStatusName(item.Status),
                            LastUpdate = item.LastUpdate,
                            TenantName = item.TenantName,
                            TenantLogo = item.TenantLogo,
                            TenantContactEmail = item.TenantContactEmail,
                            TenantContactPhone = item.TenantContactPhone,
                            TenantContactName = item.TenantContactName,
                            UserName = item.UserName,
                            UserEmail = item.UserEmail,
                            UserAccountNumber = item.UserAccountNumber
                        };

                        operationIssues.Add(operationIssue);
                    }
                }
            }
            catch (Exception e)
            {
                operationIssues = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return operationIssues;
        }

        public OperationIssue Get(Guid id)
        {
            OperationIssue operationIssue = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpoperationIssuesView
                            where x.Id == id
                            select x;

                if (query != null)
                {

                    foreach (OltpoperationIssuesView item in query)
                    {
                        operationIssue = new OperationIssue
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            TenantId = item.TenantId,
                            BranchId = item.BranchId,
                            RefId = item.RefId,
                            RefType = item.RefType,
                            RefTypeName = GetRefTypeName(item.RefType),
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.LastUpdate,
                            Details = item.Details,
                            Comments = item.Comments,
                            ContactInfo = item.ContactInfo,
                            IssueType = item.IssueType,
                            IssueTypeName = GetIssueTypeName(item.IssueType),
                            Status = item.Status,
                            StatusName = GetStatusName(item.Status),
                            LastUpdate = item.LastUpdate,
                            TenantName = item.TenantName,
                            TenantLogo = item.TenantLogo,
                            TenantContactEmail = item.TenantContactEmail,
                            TenantContactPhone = item.TenantContactPhone,
                            TenantContactName = item.TenantContactName,
                            UserName = item.UserName,
                            UserEmail = item.UserEmail,
                            UserAccountNumber = item.UserAccountNumber
                        };

                    }
                }
            }
            catch (Exception e)
            {
                operationIssue = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return operationIssue;
        }

        public bool Post(Guid? tenantId, Guid? branchId, string userId, int refType, Guid? refId, int issueType, int status, string details, string comment, string contactInfo)
        {
            bool success;
            try
            {
                OltpoperationIssues operationIssue = new OltpoperationIssues
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    BranchId = branchId,
                    UserId = userId,
                    RefType = refType,
                    RefId = refId,
                    IssueType = issueType,
                    Status = status,
                    Details = details,
                    Comments = comment,
                    ContactInfo = contactInfo,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    LastUpdate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltpoperationIssues.Add(operationIssue);
                this._businessObjects.Context.SaveChanges();

                success = true;
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        public bool Put(Guid id, string comments, int status)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpoperationIssues
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    OltpoperationIssues operationIssue = null;

                    foreach (OltpoperationIssues item in query)
                    {
                        operationIssue = item;
                    }

                    if (operationIssue != null)
                    {
                        operationIssue.Comments = comments;
                        operationIssue.Status = status;
                        operationIssue.LastUpdate = DateTime.UtcNow;
                        operationIssue.UpdatedDate = DateTime.UtcNow;

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

        public bool Put(Guid id, int status)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpoperationIssues
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    OltpoperationIssues operationIssue = null;

                    foreach (OltpoperationIssues item in query)
                    {
                        operationIssue = item;
                    }

                    if (operationIssue != null)
                    {
                        operationIssue.Status = status;
                        operationIssue.LastUpdate = DateTime.UtcNow;
                        operationIssue.UpdatedDate = DateTime.UtcNow;

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

        public bool Delete(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpoperationIssues
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    OltpoperationIssues operationIssue = null;

                    foreach (OltpoperationIssues item in query)
                    {
                        operationIssue = item;
                    }

                    if (operationIssue != null)
                    {
                        this._businessObjects.Context.OltpoperationIssues.Remove(operationIssue);
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
        /// Creates a new SurveyResponseManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public OperationIssueManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD TABLE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
