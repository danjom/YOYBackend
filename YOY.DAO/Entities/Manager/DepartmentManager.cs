using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.Department;
using YOY.DTO.Entities.Misc.Structure.POCO;
using YOY.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class DepartmentManager
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


        /// <summary>
        /// Retrieves all the departments for a given tenant
        /// </summary>
        /// <param name="activeState"></param>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public List<Department> Gets(int activeState, Guid tenantId)
        {
            List<Department> departments = new List<Department>();

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        query = from x in this._businessObjects.Context.Defdepartments
                                where x.TenantId == tenantId
                                select x;
                        break;
                    case ActiveStates.Active:
                        query = from x in this._businessObjects.Context.Defdepartments
                                where x.TenantId == tenantId && (bool)x.IsActive
                                select x;
                        break;
                    case ActiveStates.Inactive:
                        query = from x in this._businessObjects.Context.Defdepartments
                                where x.TenantId == tenantId && !(bool)x.IsActive
                                select x;
                        break;
                }

                Department department;
                foreach (Defdepartments item in query)
                {
                    department = new Department
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        Name = item.Name,
                        Description = item.Description,
                        IsActive = (bool)item.IsActive,
                        CoversLocation = item.CoversLocation,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };

                    departments.Add(department);
                }
            }
            catch (Exception e)
            {
                departments = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return departments;
        }//GETS ENDS ---------------------------------------------------------------------------------------------------------------------//

        public List<Pair<Guid, string>> Gets(Guid tenantId, int activeState)
        {
            List<Pair<Guid, string>> departments = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        query = from x in this._businessObjects.Context.Defdepartments
                                where x.TenantId == tenantId
                                select x;
                        break;
                    case ActiveStates.Active:
                        query = from x in this._businessObjects.Context.Defdepartments
                                where (bool)x.IsActive && x.TenantId == tenantId
                                select x;
                        break;
                    case ActiveStates.Inactive:
                        query = from x in this._businessObjects.Context.Defdepartments
                                where !(bool)x.IsActive && x.TenantId == tenantId
                                select x;
                        break;
                }

                if (query != null)
                {
                    Pair<Guid, string> departmentData;
                    departments = new List<Pair<Guid, string>>();

                    foreach (Defdepartments item in query)
                    {
                        departmentData = new Pair<Guid, string>
                        {
                            Key = item.Id,
                            Value = item.Name
                        };

                        departments.Add(departmentData);
                    }
                }
            }
            catch (Exception e)
            {
                departments = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return departments;
        }

        /// <summary>
        /// Retrieves a department
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Department Get(Guid id)
        {
            Department department = null;

            try
            {
                var query = from x in this._businessObjects.Context.Defdepartments
                            where x.Id == id
                            select x;

                foreach (Defdepartments item in query)
                {
                    department = new Department
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        Name = item.Name,
                        Description = item.Description,
                        IsActive = (bool)item.IsActive,
                        CoversLocation = item.CoversLocation,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };
                }
            }
            catch (Exception e)
            {
                department = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return department;
        }//GET ENDS ---------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Creates a new department
        /// </summary>
        /// <param name="name"></param>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public Department Post(string name, string description, Guid tenantId, bool coversLocation)
        {
            Department department;
            try
            {
                Defdepartments newDepartment = new Defdepartments
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    Name = name,
                    Description = description,
                    IsActive = true,
                    CoversLocation = coversLocation,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                };
                //By default on creation is active

                this._businessObjects.Context.Defdepartments.Add(newDepartment);
                this._businessObjects.Context.SaveChanges();

                department = new Department
                {
                    Id = newDepartment.Id,
                    TenantId = newDepartment.TenantId,
                    Name = newDepartment.Name,
                    Description = newDepartment.Description,
                    IsActive = (bool)newDepartment.IsActive,
                    CoversLocation = newDepartment.CoversLocation,
                    CreatedDate = newDepartment.CreatedDate,
                    UpdatedDate = newDepartment.UpdatedDate
                };
            }
            catch (Exception e)
            {
                department = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return department;
        }//POST ENDS --------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Creates a new department category relation
        /// </summary>
        /// <param name="name"></param>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public DepartmentCategory Post(Guid tenantId, Guid departmentId, Guid categoryId)
        {
            DepartmentCategory departmentCategory;
            try
            {
                DefdepartmentCategories newDepartmentCategory = new DefdepartmentCategories
                {
                    TenantId = tenantId,
                    DepartmentId = departmentId,
                    CategoryId = categoryId,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = true
                };
                //By default on creation is active

                this._businessObjects.Context.DefdepartmentCategories.Add(newDepartmentCategory);
                this._businessObjects.Context.SaveChanges();

                departmentCategory = new DepartmentCategory
                {
                    TenantId = newDepartmentCategory.TenantId,
                    DepartmentId = newDepartmentCategory.DepartmentId,
                    CategoryId = newDepartmentCategory.CategoryId,
                    IsActive = (bool)newDepartmentCategory.IsActive,
                    CreatedDate = newDepartmentCategory.CreatedDate,
                    UpdatedDate = newDepartmentCategory.UpdatedDate
                };
            }
            catch (Exception e)
            {
                departmentCategory = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return departmentCategory;
        }//POST ENDS --------------------------------------------------------------------------------------------------------------------//


        /// <summary>
        /// Updates the department active state
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Put(Guid id, int changeType)
        {
            bool sucess = false;

            try
            {
                var query = from x in this._businessObjects.Context.Defdepartments
                            where x.Id == id
                            select x;

                Defdepartments department = null;
                foreach (Defdepartments item in query)
                {
                    department = item;
                }

                if (department != null)
                {
                    switch (changeType)
                    {
                        case ChangeTypes.ActiveState:
                            department.IsActive = !department.IsActive;
                            department.UpdatedDate = DateTime.UtcNow;
                            break;
                        case ChangeTypes.Coverage:
                            department.CoversLocation = !department.CoversLocation;
                            department.UpdatedDate = DateTime.UtcNow;
                            break;
                    }

                    this._businessObjects.Context.SaveChanges();
                    sucess = true;
                }
            }
            catch (Exception e)
            {
                sucess = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return sucess;
        }//PUT ENDS ---------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Updates the department data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Department Put(Guid id, string name, string description, bool coversLocation)
        {
            Department department = null;

            try
            {
                var query = from x in this._businessObjects.Context.Defdepartments
                            where x.Id == id
                            select x;

                Defdepartments currentDpt = null;
                foreach (Defdepartments item in query)
                {
                    currentDpt = item;
                }

                if (currentDpt != null)
                {
                    currentDpt.Name = name;
                    currentDpt.Description = description;
                    currentDpt.CoversLocation = coversLocation;
                    currentDpt.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    department = new Department
                    {
                        Id = currentDpt.Id,
                        TenantId = currentDpt.TenantId,
                        Name = currentDpt.Name,
                        Description = currentDpt.Description,
                        IsActive = (bool)currentDpt.IsActive,
                        CoversLocation = currentDpt.CoversLocation,
                        CreatedDate = currentDpt.CreatedDate,
                        UpdatedDate = currentDpt.UpdatedDate
                    };
                }
            }
            catch (Exception e)
            {
                department = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return department;
        }//PUT ENDS -------------------------------------------------------------------------------------------------------------------//


        /// <summary>
        /// Deletes a department
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Defdepartments
                            where x.Id == id
                            select x;

                Defdepartments department = null;
                foreach (Defdepartments item in query)
                {
                    department = item;
                }

                if (department != null)
                {
                    this._businessObjects.Context.Defdepartments.Remove(department);
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

        /// <summary>
        /// Deletes a department category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool Delete(Guid tenantId, Guid departmentId, Guid categoryId)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.DefdepartmentCategories
                            where x.TenantId == tenantId && x.DepartmentId == departmentId && x.CategoryId == categoryId
                            select x;

                DefdepartmentCategories departmentCategory = null;
                foreach (DefdepartmentCategories item in query)
                {
                    departmentCategory = item;
                }

                if (departmentCategory != null)
                {
                    this._businessObjects.Context.DefdepartmentCategories.Remove(departmentCategory);
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

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new FloorManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public DepartmentManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD FLOOR MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}