using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.Values;
using YOY.Values.Strings;
using YOY.DAO.Entities.DB;
using Microsoft.EntityFrameworkCore.Metadata;
using YOY.Security;
using YOY.DTO.Entities.Misc.Employee;

namespace YOY.DAO.Entities.Manager
{
    public class EmployeeManager
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
        private static readonly string[] roles = { "Admin", "Operator" };

        #endregion


        private string GetCurrencyTypeName(int currencyType)
        {
            string typeName = currencyType switch
            {
                CurrencyTypes.CostaRicanColon => Resources.OperatorCostaRicanColonCurrency,
                CurrencyTypes.USDollar => Resources.OperatorUSDollarCurrency,
                CurrencyTypes.GuatemalanQuetzal => Resources.OperatorGuatemalanQuetzalCurrency,
                CurrencyTypes.HonduranLempira => Resources.OperatorHonduranLempiraCurrency,
                CurrencyTypes.NicaraguanCordoba => Resources.OperatorNicaraguanCordobaCurrency,
                CurrencyTypes.MexicanPeso => Resources.OperatorMexicanPesoCurrency,
                CurrencyTypes.ColombianPeso => Resources.OperatorColombianPesoCurrency,
                _ => "--",
            };
            return typeName;

        }

        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS METHODS                                                                                                                                  //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        #region METHODS

        /// <summary>
        /// Retrieves all employees of tenant
        /// </summary>
        /// <returns></returns>
        public List<Employee> Gets()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                var query = from x in this._businessObjects.Context.OltpemployeesView
                            where x.TenantId == this._businessObjects.Tenant.TenantId
                            orderby x.Name ascending, x.RoleId ascending
                            select x;

                Employee employee;

                foreach (OltpemployeesView item in query)
                {

                    employee = new Employee
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        BranchId = item.BranchId,
                        CreatorId = item.CreatorId,
                        RoleId = item.RoleId,
                        AuthorizedValidatorPhoneNumber = item.AuthorizedValidatorPhoneNumber,
                        MembershipId = item.MembershipId,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        AccessAllowed = item.AccessAllowed,
                        UserId = item.UserId,
                        UserName = item.UserName,
                        Name = item.Name,
                        RoleName = item.RoleName,
                        CurrencySymbol = item.CurrencySymbol,
                        CurrencyTypeName = GetCurrencyTypeName(item.CurrencyType),
                        BranchName = item.TenantName + " -- " + item.BrachName
                    };

                    employees.Add(employee);
                }
            }
            catch (Exception e)
            {
                employees = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return employees;
        }//METHOD GETS ENDS ----------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Retrieve all employee roles from a user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<Employee> Gets(string userKey, int keyType, int activeState)
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                var query = (dynamic)null;

                switch (keyType)
                {
                    case UserKeys.UserId:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                query = from x in this._businessObjects.Context.OltpemployeesView
                                        where x.UserId == userKey
                                        orderby x.BranchId ascending
                                        select x;
                                break;
                            case ActiveStates.Active:
                                query = from x in this._businessObjects.Context.OltpemployeesView
                                        where x.AccessAllowed && x.UserId == userKey
                                        orderby x.BranchId ascending
                                        select x;
                                break;
                            case ActiveStates.Inactive:
                                query = from x in this._businessObjects.Context.OltpemployeesView
                                        where !x.AccessAllowed && x.UserId == userKey
                                        orderby x.BranchId ascending
                                        select x;
                                break;
                        }

                        break;
                    case UserKeys.Username:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                query = from x in this._businessObjects.Context.OltpemployeesView
                                        where x.UserName == userKey
                                        orderby x.BranchId ascending
                                        select x;
                                break;
                            case ActiveStates.Active:
                                query = from x in this._businessObjects.Context.OltpemployeesView
                                        where x.AccessAllowed && x.UserName == userKey
                                        orderby x.BranchId ascending
                                        select x;
                                break;
                            case ActiveStates.Inactive:
                                query = from x in this._businessObjects.Context.OltpemployeesView
                                        where !x.AccessAllowed && x.UserName == userKey
                                        orderby x.BranchId ascending
                                        select x;
                                break;
                        }

                        break;
                }

                Employee employee;
                foreach (OltpemployeesView item in query)
                {

                    employee = new Employee
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        BranchId = item.BranchId,
                        CreatorId = item.CreatorId,
                        RoleId = item.RoleId,
                        AuthorizedValidatorPhoneNumber = item.AuthorizedValidatorPhoneNumber,
                        MembershipId = item.MembershipId,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        AccessAllowed = item.AccessAllowed,
                        AccessKey = item.AccessKey,
                        UserId = item.UserId,
                        UserName = item.UserName,
                        Name = item.Name,
                        RoleName = item.RoleName,
                        CurrencySymbol = item.CurrencySymbol,
                        CurrencyTypeName = GetCurrencyTypeName(item.CurrencyType),
                        BranchName = item.TenantName + " -- " + item.BrachName

                    };


                    employees.Add(employee);
                }
            }
            catch (Exception e)
            {
                employees = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return employees;
        }//METHOD GETS ENDS ----------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Retrieve a specific subscription
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <returns></returns>
        public Employee Get(Guid employeeId, bool filterByTenant)
        {
            Employee employee = null;

            try
            {
                var query = (dynamic)null;


                if (filterByTenant)
                {
                    query = from x in this._businessObjects.Context.OltpemployeesView
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == employeeId
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.OltpemployeesView
                            where x.Id == employeeId
                            select x;
                }

                foreach (OltpemployeesView item in query)
                {
                    employee = new Employee
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        BranchId = item.BranchId,
                        CreatorId = item.CreatorId,
                        RoleId = item.RoleId,
                        AuthorizedValidatorPhoneNumber = item.AuthorizedValidatorPhoneNumber,
                        MembershipId = item.MembershipId,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        AccessAllowed = item.AccessAllowed,
                        UserId = item.UserId,
                        UserName = item.UserName,
                        Name = item.Name,
                        RoleName = item.RoleName,
                        CurrencySymbol = item.CurrencySymbol,
                        CurrencyTypeName = GetCurrencyTypeName(item.CurrencyType),
                        BranchName = item.TenantName + " -- " + item.BrachName
                    };

                }

            }
            catch (Exception e)
            {
                employee = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return employee;
        }//METHOD GET ENDS ------------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// Checks if the username is registered with the role param and has access to the management side
        /// </summary>
        /// <param name="username"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool Get(string username, string roleName)
        {
            bool exists = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpemployeesView
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.UserName == username && x.RoleName == roleName && x.AccessAllowed
                            select x;

                foreach (OltpemployeesView item in query)
                {
                    exists = true;

                }

            }
            catch (Exception e)
            {
                exists = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return exists;
        }//METHOD GET ENDS ------------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// Retrieve a specific subscription
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <returns></returns>
        public Employee Get(string phoneNumber, string roleName, int maxEmployees)
        {
            Employee employee = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpemployeesView
                            where x.AuthorizedValidatorPhoneNumber == phoneNumber && x.RoleName == roleName
                            select x;

                int count = 0;

                foreach (OltpemployeesView item in query)
                {
                    ++count;

                    employee = new Employee
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        BranchId = item.BranchId,
                        CreatorId = item.CreatorId,
                        RoleId = item.RoleId,
                        AuthorizedValidatorPhoneNumber = item.AuthorizedValidatorPhoneNumber,
                        MembershipId = item.MembershipId,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        AccessAllowed = item.AccessAllowed,
                        UserId = item.UserId,
                        UserName = item.UserName,
                        Name = item.Name,
                        RoleName = item.RoleName,
                        CurrencySymbol = item.CurrencySymbol,
                        CurrencyTypeName = GetCurrencyTypeName(item.CurrencyType),
                        BranchName = item.TenantName + " -- " + item.BrachName
                    };

                }

                if(count > maxEmployees)
                {
                    //Marked as invalid
                    employee = null;
                }

            }
            catch (Exception e)
            {
                employee = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return employee;
        }//METHOD GET ENDS ------------------------------------------------------------------------------------------------------------------------------ //


        public Employee Get(string sk, int employeeSK, bool filterByTenant)
        {
            Employee employee = null;

            try
            {

                var query = (dynamic)null;

                switch (employeeSK)
                {
                    case EmployeeSks.Username:
                        if (filterByTenant)
                        {
                            query = from x in this._businessObjects.Context.OltpemployeesView
                                    where x.TenantId == this._businessObjects.Tenant.TenantId && x.UserName == sk
                                    select x;
                        }
                        else
                        {
                            query = from x in this._businessObjects.Context.OltpemployeesView
                                    where x.UserName == sk
                                    select x;
                        }

                        break;
                    case EmployeeSks.AccessKey:
                        if (filterByTenant)
                        {
                            query = from x in this._businessObjects.Context.OltpemployeesView
                                    where x.TenantId == this._businessObjects.Tenant.TenantId && x.AccessKey == sk
                                    select x;
                        }
                        else
                        {
                            query = from x in this._businessObjects.Context.OltpemployeesView
                                    where x.AccessKey == sk
                                    select x;
                        }

                        break;
                }

                if (query != null)
                {
                    foreach (OltpemployeesView item in query)
                    {
                        employee = new Employee
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            BranchId = item.BranchId,
                            CreatorId = item.CreatorId,
                            RoleId = item.RoleId,
                            AuthorizedValidatorPhoneNumber = item.AuthorizedValidatorPhoneNumber,
                            MembershipId = item.MembershipId,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            AccessAllowed = item.AccessAllowed,
                            UserId = item.UserId,
                            UserName = item.UserName,
                            Name = item.Name,
                            RoleName = item.RoleName,
                            AccessKey = item.AccessKey,
                            CurrencySymbol = item.CurrencySymbol,
                            CurrencyTypeName = GetCurrencyTypeName(item.CurrencyType),
                            BranchName = item.TenantName + " -- " + item.BrachName
                        };
                    }
                }

            }
            catch (Exception e)
            {
                employee = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return employee;
        }//METHOD GET ENDS ------------------------------------------------------------------------------------------------------------------------------ //



        /// <summary>
        /// Creates a new employee for a membership to a tenant
        /// and sets its role
        /// </summary>
        /// <param name="membershipId"></param>
        /// <param name="userId"></param>
        /// <param name="creatorId"></param>
        /// <param name="branchId"></param>
        /// <param name="rolePos"></param>
        /// <returns></returns>
        public bool Post(Guid membershipId, string userId, Guid? creatorId, Guid? branchId, int rolePos, string authorizedValidatorPhoneNumber)
        {
            bool success = false;


            AspNetRoles role = null;
            AspNetUserRoles userRole = null;
            Oltpemployees employee = null;

            try
            {

                string roleName = roles.ElementAt(rolePos);

                var query = from x in this._businessObjects.Context.AspNetRoles
                            where x.Name == roleName
                            select x;

                foreach (AspNetRoles currentRole in query)
                {
                    role = currentRole;
                }

                if (role != null)
                {
                    var queryUserRole = from x in this._businessObjects.Context.AspNetUserRoles
                                        where x.RoleId == role.Id && x.UserId == userId
                                        select x;

                    foreach (AspNetUserRoles currentUserRole in queryUserRole)
                    {
                        userRole = currentUserRole;
                    }

                    //In case the user doesn't the role, needs to be assigned
                    if (userRole == null)
                    {
                        userRole = new AspNetUserRoles
                        {
                            UserId = userId,
                            RoleId = role.Id
                        };

                        this._businessObjects.Context.AspNetUserRoles.Add(userRole);
                    }


                    employee = new Oltpemployees
                    {

                        Id = Guid.NewGuid(),
                        MembershipId = membershipId,
                        RoleId = role.Id,
                        TenantId = this._businessObjects.Tenant.TenantId,
                        BranchId = branchId,
                        CreatorId = creatorId,
                        AuthorizedValidatorPhoneNumber = authorizedValidatorPhoneNumber,
                        AccessAllowed = true,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    };

                    Checksums ch = new Checksums();
                    Random random = new Random();
                    string accessBase = employee.MembershipId.ToString() + random.Next() + "?" + employee.CreatorId.ToString() + "/" + random.NextDouble() * random.Next() + employee.RoleId.ToString();
                    employee.AccessKey = ch.Compute(accessBase, DigestAlgorithm.SHA256);


                    this._businessObjects.Context.Oltpemployees.Add(employee);
                    this._businessObjects.Context.SaveChanges();


                    success = true;


                }

            }
            catch (Exception e)
            {
                this._businessObjects.Context.AspNetUserRoles.Remove(userRole);
                this._businessObjects.Context.Oltpemployees.Remove(employee);
                this._businessObjects.Context.SaveChanges();

                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }//METHOD POST ENDS ----------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Changes user active state
        /// </summary>
        /// <param name="username"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public bool Put(Guid id)
        {
            bool success = false;


            try
            {
                Oltpemployees employee = null;

                var query = from x in this._businessObjects.Context.Oltpemployees
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;

                foreach (Oltpemployees item in query)
                {
                    employee = item;
                }

                if (employee != null)
                {
                    employee.AccessAllowed = !employee.AccessAllowed;
                    employee.UpdatedDate = DateTime.UtcNow;

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
        }//METHOD PUT ENDS ------------------------------------------------------------------------------------------------------------------------------ //



        /// <summary>
        /// Updates an Employee
        /// </summary>
        /// <param name="username"></param>
        /// <param name="loyaltyPoints"></param>
        /// <returns></returns>
        public Employee Put(Guid id, string roleId, string authorizedValidatorPhoneNumber)
        {
            Employee currentEmployee = null;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpemployees
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;

                Oltpemployees employee = null;

                foreach (Oltpemployees item in query)
                {
                    employee = item;
                }

                if (employee != null)
                {
                    employee.RoleId = roleId;
                    employee.AuthorizedValidatorPhoneNumber = authorizedValidatorPhoneNumber;
                    employee.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    OltpemployeesView employeeView = (from x in this._businessObjects.Context.OltpemployeesView
                                                         where x.Id == employee.Id
                                                         select x).FirstOrDefault();

                    if(employeeView != null)
                    {
                        currentEmployee = new Employee
                        {
                            Id = employeeView.Id,
                            TenantId = employeeView.TenantId,
                            BranchId = employeeView.BranchId,
                            CreatorId = employeeView.CreatorId,
                            CreatedDate = employeeView.CreatedDate,
                            UpdatedDate = employeeView.UpdatedDate,
                            MembershipId = employeeView.MembershipId,
                            RoleId = employeeView.RoleId,
                            AccessAllowed = employeeView.AccessAllowed,
                            Name = "",// user.Name,
                            CurrencySymbol = employeeView.CurrencySymbol,
                            CurrencyTypeName = GetCurrencyTypeName(employeeView.CurrencyType),
                            RoleName = employeeView.RoleName
                        };
                    }
                    else
                    {
                        currentEmployee = new Employee();
                    }
                }

            }
            catch (Exception e)
            {
                currentEmployee = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return currentEmployee;
        }//METHOD PUT ENDS ------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Deletes a subscription
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public EmployeeToDelete Delete(Guid id)
        {
            EmployeeToDelete toDelete = new EmployeeToDelete();

            try
            {
                Oltpemployees employee = null;

                var query = from x in this._businessObjects.Context.Oltpemployees
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;

                foreach (Oltpemployees item in query)
                {
                    employee = item;
                }

                if (employee != null)
                {
                    OltpemployeesView employeeView = (from x in this._businessObjects.Context.OltpemployeesView
                                                      where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                                                      select x).FirstOrDefault();

                    if(employeeView != null)
                    {

                        toDelete.UserId = employeeView.UserId;
                        toDelete.Role = employeeView.RoleName;

                        this._businessObjects.Context.Oltpemployees.Remove(employee);
                        this._businessObjects.Context.SaveChanges();
                    }

                }
            }
            catch (Exception e)
            {
                toDelete = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return toDelete;
        }//METHODS DELETE ENDS -------------------------------------------------------------------------------------------------------------------------- //

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
        public EmployeeManager(BusinessObjects businessObjects)
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
