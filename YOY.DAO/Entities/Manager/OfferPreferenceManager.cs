using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.OfferPreference;
using YOY.Values;
using YOY.Values.Strings;

namespace YOY.DAO.Entities.Manager
{
    public class OfferPreferenceManager
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

        public string GetInputTypeName(int inputType)
        {
            string typeName = inputType switch
            {
                PreferenceInputTypes.RadioButton => Resources.RadioButton,
                PreferenceInputTypes.Checkbox => Resources.Checkbox,
                PreferenceInputTypes.Dropdown => Resources.Dropdown,
                PreferenceInputTypes.ColorPicker => Resources.ColorPicker,
                PreferenceInputTypes.TagsPicker => Resources.TagsPicker,
                _ => "--",
            };
            return typeName;

        }


        public List<OfferPreference> Gets(Guid offerId, int activeState)
        {
            List<OfferPreference> offerPreferences = null;

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        query = from x in this._businessObjects.Context.OltpofferPreferences
                                where x.OfferId == offerId
                                select x;
                        break;
                    case ActiveStates.Active:
                        query = from x in this._businessObjects.Context.OltpofferPreferences
                                where (bool)x.IsActive && x.OfferId == offerId
                                select x;
                        break;
                    case ActiveStates.Inactive:
                        query = from x in this._businessObjects.Context.OltpofferPreferences
                                where !(bool)x.IsActive && x.OfferId == offerId
                                select x;
                        break;
                }

                if (query != null)
                {
                    OfferPreference offerPreference;
                    offerPreferences = new List<OfferPreference>();

                    foreach (OltpofferPreferences item in query)
                    {
                        offerPreference = new OfferPreference
                        {
                            Id = item.Id,
                            OfferId = item.OfferId,
                            Name = item.Name,
                            Hint = item.Hint,
                            InputType = item.InputType,
                            InputTypeName = GetInputTypeName(item.InputType),
                            MinOptionsSelected = item.MinOptionsSelected,
                            MaxOptionsSelected = item.MaxOptionsSelected,
                            Mandatory = item.Mandatory,
                            IsActive = (bool)item.IsActive,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        offerPreferences.Add(offerPreference);
                    }
                }
            }
            catch (Exception e)
            {
                offerPreferences = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offerPreferences;
        }

        public OfferPreference Get(Guid id)
        {
            OfferPreference offerPreference = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpofferPreferences
                            where x.Id == id
                            select x;


                if (query != null)
                {

                    foreach (OltpofferPreferences item in query)
                    {
                        offerPreference = new OfferPreference
                        {
                            Id = item.Id,
                            OfferId = item.OfferId,
                            Name = item.Name,
                            Hint = item.Hint,
                            InputType = item.InputType,
                            InputTypeName = GetInputTypeName(item.InputType),
                            MinOptionsSelected = item.MinOptionsSelected,
                            MaxOptionsSelected = item.MaxOptionsSelected,
                            Mandatory = item.Mandatory,
                            IsActive = (bool)item.IsActive,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                offerPreference = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offerPreference;
        }

        public OfferPreference Post(Guid offerId, string name, string hint, int inputType, int minOptionsSelected, int maxOptionsSelected, bool mandatory)
        {
            OfferPreference offerPreference;

            try
            {
                OltpofferPreferences newOfferPreference = new OltpofferPreferences
                {
                    Id = Guid.NewGuid(),
                    OfferId = offerId,
                    Name = name,
                    Hint = hint,
                    InputType = inputType,
                    MinOptionsSelected = minOptionsSelected,
                    MaxOptionsSelected = maxOptionsSelected,
                    Mandatory = mandatory,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltpofferPreferences.Add(newOfferPreference);
                this._businessObjects.Context.SaveChanges();

                offerPreference = new OfferPreference
                {
                    Id = newOfferPreference.Id,
                    OfferId = newOfferPreference.OfferId,
                    Name = newOfferPreference.Name,
                    Hint = newOfferPreference.Hint,
                    InputType = newOfferPreference.InputType,
                    InputTypeName = GetInputTypeName(newOfferPreference.InputType),
                    MinOptionsSelected = newOfferPreference.MinOptionsSelected,
                    MaxOptionsSelected = newOfferPreference.MaxOptionsSelected,
                    Mandatory = newOfferPreference.Mandatory,
                    IsActive = (bool)newOfferPreference.IsActive,
                    CreatedDate = newOfferPreference.CreatedDate,
                    UpdatedDate = newOfferPreference.UpdatedDate
                };
            }
            catch (Exception e)
            {
                offerPreference = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offerPreference;
        }

        public OfferPreference Put(Guid id, string name, string hint, int inputType, int minOptionsSelected, int maxOptionsSelected, bool mandatory)
        {
            OfferPreference offerPreference = null;

            try
            {

                var query = from x in this._businessObjects.Context.OltpofferPreferences
                            where x.Id == id
                            select x;

                if (query != null)
                {

                    OltpofferPreferences currentPreference = null;

                    foreach (OltpofferPreferences item in query)
                    {
                        currentPreference = item;
                    }

                    if (currentPreference != null)
                    {
                        currentPreference.Name = name;
                        currentPreference.Hint = hint;
                        currentPreference.InputType = inputType;
                        currentPreference.MinOptionsSelected = minOptionsSelected;
                        currentPreference.MaxOptionsSelected = maxOptionsSelected;
                        currentPreference.Mandatory = mandatory;

                        this._businessObjects.Context.SaveChanges();

                        offerPreference = new OfferPreference
                        {
                            Id = currentPreference.Id,
                            OfferId = currentPreference.OfferId,
                            Name = currentPreference.Name,
                            Hint = currentPreference.Hint,
                            InputType = currentPreference.InputType,
                            InputTypeName = GetInputTypeName(currentPreference.InputType),
                            MinOptionsSelected = currentPreference.MinOptionsSelected,
                            MaxOptionsSelected = currentPreference.MaxOptionsSelected,
                            Mandatory = currentPreference.Mandatory,
                            IsActive = (bool)currentPreference.IsActive,
                            CreatedDate = currentPreference.CreatedDate,
                            UpdatedDate = currentPreference.UpdatedDate
                        };
                    }

                }


            }
            catch (Exception e)
            {
                offerPreference = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offerPreference;
        }

        public bool Put(Guid id, int changeType)
        {
            bool success = false;

            try
            {

                var query = from x in this._businessObjects.Context.OltpofferPreferences
                            where x.Id == id
                            select x;

                if (query != null)
                {

                    OltpofferPreferences currentPreference = null;

                    foreach (OltpofferPreferences item in query)
                    {
                        currentPreference = item;
                    }

                    if (currentPreference != null)
                    {
                        switch (changeType)
                        {
                            case ChangeTypes.ActiveState:
                                currentPreference.IsActive = !currentPreference.IsActive;
                                currentPreference.UpdatedDate = DateTime.UtcNow;

                                this._businessObjects.Context.SaveChanges();
                                success = true;
                                break;
                            case ChangeTypes.MandatoryStatus:
                                currentPreference.Mandatory = !currentPreference.Mandatory;
                                currentPreference.UpdatedDate = DateTime.UtcNow;

                                this._businessObjects.Context.SaveChanges();
                                success = true;
                                break;
                        }
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

                OltpofferPreferences currentPreference = (from x in this._businessObjects.Context.OltpofferPreferences
                                                            where x.Id == id
                                                            select x).FirstOrDefault();

                if (currentPreference != null)
                {
                    this._businessObjects.Context.OltpofferPreferences.Remove(currentPreference);
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

        #region OPTIONS

        public List<OfferPreferenceOption> Gets(Guid preferenceId, Guid? offerId, int activeState)
        {
            List<OfferPreferenceOption> offerPreferenceOptions = null;

            try
            {
                var query = (dynamic)null;

                if (offerId != null)
                {
                    switch (activeState)
                    {
                        case ActiveStates.All:
                            query = from x in this._businessObjects.Context.OltpofferPreferenceOptions
                                    where x.OfferId == offerId && x.PreferenceId == preferenceId
                                    select x;
                            break;
                        case ActiveStates.Active:
                            query = from x in this._businessObjects.Context.OltpofferPreferenceOptions
                                    where (bool)x.IsActive && x.OfferId == offerId && x.PreferenceId == preferenceId
                                    select x;
                            break;
                        case ActiveStates.Inactive:
                            query = from x in this._businessObjects.Context.OltpofferPreferenceOptions
                                    where !(bool)x.IsActive && x.OfferId == offerId && x.PreferenceId == preferenceId
                                    select x;
                            break;
                    }
                }
                else
                {
                    switch (activeState)
                    {
                        case ActiveStates.All:
                            query = from x in this._businessObjects.Context.OltpofferPreferenceOptions
                                    where x.PreferenceId == preferenceId
                                    select x;
                            break;
                        case ActiveStates.Active:
                            query = from x in this._businessObjects.Context.OltpofferPreferenceOptions
                                    where (bool)x.IsActive && x.PreferenceId == preferenceId
                                    select x;
                            break;
                        case ActiveStates.Inactive:
                            query = from x in this._businessObjects.Context.OltpofferPreferenceOptions
                                    where !(bool)x.IsActive && x.PreferenceId == preferenceId
                                    select x;
                            break;
                    }
                }


                if (query != null)
                {
                    OfferPreferenceOption offerPreferenceOption;
                    offerPreferenceOptions = new List<OfferPreferenceOption>();

                    foreach (OltpofferPreferenceOptions item in query)
                    {
                        offerPreferenceOption = new OfferPreferenceOption
                        {
                            Id = item.Id,
                            PreferenceId = item.PreferenceId,
                            OfferId = item.OfferId,
                            ImageId = item.ImageId,
                            Value = item.Value,
                            Price = item.Price,
                            RegularPrice = item.RegularPrice,
                            ReplacesOfferImgOnSelect = item.ReplacesOfferImgOnSelect,
                            IsActive = (bool)item.IsActive,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        offerPreferenceOptions.Add(offerPreferenceOption);
                    }
                }
            }
            catch (Exception e)
            {
                offerPreferenceOptions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offerPreferenceOptions;
        }

        public OfferPreferenceOption Get(Guid id, Guid? preferenceId)
        {
            OfferPreferenceOption offerPreferenceOption = null;

            try
            {
                var query = (dynamic)null;

                if (preferenceId != null)
                {
                    query = from x in this._businessObjects.Context.OltpofferPreferenceOptions
                            where x.PreferenceId == preferenceId && x.Id == id
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.OltpofferPreferenceOptions
                            where x.Id == id
                            select x;
                }


                if (query != null)
                {

                    foreach (OltpofferPreferenceOptions item in query)
                    {
                        offerPreferenceOption = new OfferPreferenceOption
                        {
                            Id = item.Id,
                            PreferenceId = item.PreferenceId,
                            OfferId = item.OfferId,
                            ImageId = item.ImageId,
                            Value = item.Value,
                            Price = item.Price,
                            RegularPrice = item.RegularPrice,
                            ReplacesOfferImgOnSelect = item.ReplacesOfferImgOnSelect,
                            IsActive = (bool)item.IsActive,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                offerPreferenceOption = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offerPreferenceOption;
        }



        public OfferPreferenceOption Post(Guid preferenceId, Guid offerId, Guid? imgId, string value, decimal price, decimal? regularPrice, bool replacesImgOnSelect)
        {
            OfferPreferenceOption offerPreferenceOption;

            try
            {
               
                OltpofferPreferenceOptions newOfferPreferenceOption = new OltpofferPreferenceOptions
                {
                    Id = Guid.NewGuid(),
                    PreferenceId = preferenceId,
                    OfferId = offerId,
                    ImageId = imgId,
                    Value = value,
                    Price = price,
                    RegularPrice = regularPrice,
                    ReplacesOfferImgOnSelect = replacesImgOnSelect,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltpofferPreferenceOptions.Add(newOfferPreferenceOption);
                this._businessObjects.Context.SaveChanges();

                offerPreferenceOption = new OfferPreferenceOption
                {
                    Id = newOfferPreferenceOption.Id,
                    PreferenceId = newOfferPreferenceOption.PreferenceId,
                    OfferId = newOfferPreferenceOption.OfferId,
                    ImageId = newOfferPreferenceOption.ImageId,
                    Value = newOfferPreferenceOption.Value,
                    Price = newOfferPreferenceOption.Price,
                    RegularPrice = newOfferPreferenceOption.RegularPrice,
                    ReplacesOfferImgOnSelect = newOfferPreferenceOption.ReplacesOfferImgOnSelect,
                    IsActive = (bool)newOfferPreferenceOption.IsActive,
                    CreatedDate = newOfferPreferenceOption.CreatedDate,
                    UpdatedDate = newOfferPreferenceOption.UpdatedDate
                };

            }
            catch (Exception e)
            {
                offerPreferenceOption = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offerPreferenceOption;
        }

        public List<OfferPreferenceOption> Post(List<NewPreferenceOption> newOptions)
        {
            List<OfferPreferenceOption> offerPreferenceOptions;

            try
            {
                offerPreferenceOptions = new List<OfferPreferenceOption>();
                OltpofferPreferenceOptions newOfferPreferenceOption;
                OfferPreferenceOption offerPreferenceOption;

                foreach (NewPreferenceOption item in newOptions)
                {
                    newOfferPreferenceOption = new OltpofferPreferenceOptions
                    {
                        Id = Guid.NewGuid(),
                        PreferenceId = item.PreferenceId,
                        OfferId = item.OfferId,
                        ImageId = item.ImageId,
                        Value = item.Value,
                        Price = item.Price,
                        RegularPrice = item.RegularPrice,
                        ReplacesOfferImgOnSelect = item.ReplacesOfferImageOnSelect,
                        IsActive = true,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    };

                    this._businessObjects.Context.OltpofferPreferenceOptions.Add(newOfferPreferenceOption);

                    this._businessObjects.Context.SaveChanges();


                    offerPreferenceOption = new OfferPreferenceOption
                    {
                        Id = newOfferPreferenceOption.Id,
                        PreferenceId = newOfferPreferenceOption.PreferenceId,
                        OfferId = newOfferPreferenceOption.OfferId,
                        ImageId = newOfferPreferenceOption.ImageId,
                        Value = newOfferPreferenceOption.Value,
                        Price = newOfferPreferenceOption.Price,
                        RegularPrice = newOfferPreferenceOption.RegularPrice,
                        ReplacesOfferImgOnSelect = newOfferPreferenceOption.ReplacesOfferImgOnSelect,
                        IsActive = (bool)newOfferPreferenceOption.IsActive,
                        CreatedDate = newOfferPreferenceOption.CreatedDate,
                        UpdatedDate = newOfferPreferenceOption.UpdatedDate
                    };

                    offerPreferenceOptions.Add(offerPreferenceOption);
                }

            }
            catch (Exception e)
            {
                offerPreferenceOptions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offerPreferenceOptions;
        }

        public List<OfferPreferenceOption> Put(List<UpdatedPreferenceOption> updatedOptions)
        {
            List<OfferPreferenceOption> offerPreferenceOptions = null;

            try
            {
                OltpofferPreferenceOptions currentOption = (dynamic)null;
                bool dbUpdated = false;

                if (updatedOptions?.Count > 0)
                {
                    offerPreferenceOptions = new List<OfferPreferenceOption>();
                    OfferPreferenceOption offerPreferenceOption;

                    foreach (UpdatedPreferenceOption item in updatedOptions)
                    {
                        currentOption = (from x in this._businessObjects.Context.OltpofferPreferenceOptions
                                        where x.Id == item.Id
                                        select x).FirstOrDefault();

                        if(currentOption != null)
                        {
                            currentOption.Value = item.Value;
                            currentOption.Price = item.Price;
                            currentOption.RegularPrice = item.RegularPrice;
                            currentOption.ImageId = item.ImageId;
                            currentOption.ReplacesOfferImgOnSelect = item.ReplacesOfferImageOnSelect;

                            dbUpdated = true;

                            offerPreferenceOption = new OfferPreferenceOption
                            {
                                Id = currentOption.Id,
                                PreferenceId = currentOption.PreferenceId,
                                OfferId = currentOption.OfferId,
                                ImageId = currentOption.ImageId,
                                Value = currentOption.Value,
                                Price = currentOption.Price,
                                RegularPrice = currentOption.RegularPrice,
                                ReplacesOfferImgOnSelect = currentOption.ReplacesOfferImgOnSelect,
                                IsActive = (bool)currentOption.IsActive,
                                CreatedDate = currentOption.CreatedDate,
                                UpdatedDate = currentOption.UpdatedDate
                            };

                            offerPreferenceOptions.Add(offerPreferenceOption);

                        }
                    }

                    if(dbUpdated)
                        this._businessObjects.Context.SaveChanges();
                }

            }
            catch (Exception e)
            {
                offerPreferenceOptions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offerPreferenceOptions;
        }

        public bool Put(Guid id, Guid? preferenceId, int changeType)
        {
            bool success = false;

            try
            {

                var query = (dynamic)null;

                if (preferenceId != null)
                {
                    query = from x in this._businessObjects.Context.OltpofferPreferenceOptions
                            where x.PreferenceId == preferenceId && x.Id == id
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.OltpofferPreferenceOptions
                            where x.Id == id
                            select x;
                }

                if (query != null)
                {

                    OltpofferPreferenceOptions currentPreferenceOption = null;

                    foreach (OltpofferPreferenceOptions item in query)
                    {
                        currentPreferenceOption = item;
                    }

                    if (currentPreferenceOption != null)
                    {
                        switch (changeType)
                        {
                            case ChangeTypes.ActiveState:
                                currentPreferenceOption.IsActive = !currentPreferenceOption.IsActive;
                                currentPreferenceOption.UpdatedDate = DateTime.UtcNow;

                                this._businessObjects.Context.SaveChanges();
                                success = true;
                                break;
                            case ChangeTypes.ReplacesImageStatus:
                                currentPreferenceOption.ReplacesOfferImgOnSelect = !currentPreferenceOption.ReplacesOfferImgOnSelect;
                                currentPreferenceOption.UpdatedDate = DateTime.UtcNow;

                                this._businessObjects.Context.SaveChanges();
                                success = true;
                                break;
                        }
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

        /// <summary>
        /// Changes preference option display img
        /// </summary>
        /// <param name="id"></param>
        /// <param name="imageId"></param>
        /// <returns></returns>
        public Guid? Put(Guid id, Guid imageId)
        {
            Guid? currentImg = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpofferPreferenceOptions
                            where x.Id == id
                            select x;

                OltpofferPreferenceOptions offerPreferenceOption = null;
                foreach (OltpofferPreferenceOptions item in query)
                {
                    offerPreferenceOption = item;
                }


                if (offerPreferenceOption != null)
                {
                    currentImg = offerPreferenceOption.ImageId;
                    offerPreferenceOption.ImageId = imageId;
                    offerPreferenceOption.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                }

            }
            catch (Exception e)
            {
                currentImg = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return currentImg;
        }//METHOD PUT ENDS ------------------------------------------------------------------------------------------------------------------------------ //


        public bool Delete(List<ToDeletePreferenceOption> toDeletePreferenceOptions)
        {
            bool success = false;

            try
            {

                OltpofferPreferenceOptions currentOption;
                bool dbUpdated = false;

                if(toDeletePreferenceOptions?.Count > 0)
                {
                    foreach(ToDeletePreferenceOption item in toDeletePreferenceOptions)
                    {
                        if (item.PreferenceId != null)
                        {
                            currentOption = (from x in this._businessObjects.Context.OltpofferPreferenceOptions
                                             where x.PreferenceId == item.PreferenceId && x.Id == item.Id
                                             select x).FirstOrDefault();
                        }
                        else
                        {
                            currentOption = (from x in this._businessObjects.Context.OltpofferPreferenceOptions
                                             where x.Id == item.Id
                                             select x).FirstOrDefault();
                        }

                        if(currentOption != null)
                        {
                            this._businessObjects.Context.OltpofferPreferenceOptions.Remove(currentOption);

                            dbUpdated = true;
                        }
                    }

                    if (dbUpdated)
                    {
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

        public bool Delete(Guid id, Guid? preferenceId)
        {
            bool success = false;

            try
            {

                OltpofferPreferenceOptions currentOption;

                if (preferenceId != null)
                {
                    currentOption = (from x in this._businessObjects.Context.OltpofferPreferenceOptions
                                     where x.PreferenceId == preferenceId && x.Id == id
                                     select x).FirstOrDefault();
                }
                else
                {
                    currentOption = (from x in this._businessObjects.Context.OltpofferPreferenceOptions
                                     where x.Id == id
                                     select x).FirstOrDefault();
                }

                if (currentOption != null)
                {
                    this._businessObjects.Context.OltpofferPreferenceOptions.Remove(currentOption);
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

        #region FULLOFFERPREFERENCES

        public List<OfferPreferenceWithOptions> Gets(Guid offerId, Guid? preferenceId)
        {
            List<OfferPreferenceWithOptions> preferencesWithOptions = null;

            try
            {

                var query = (dynamic)null;

                if (preferenceId != null)
                {
                    query = from x in this._businessObjects.Context.OltppreferenceWithOptionView
                            where x.OfferId == offerId && x.PreferenceId == preferenceId
                            orderby x.PreferenceInputType
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.OltppreferenceWithOptionView
                            where x.OfferId == offerId
                            orderby x.PreferenceInputType
                            select x;
                }
                    

                if (query != null)
                {
                    List<FlatOfferPreferenceWithOptionData> flatOfferPreferenceOptions = new List<FlatOfferPreferenceWithOptionData>();
                    FlatOfferPreferenceWithOptionData flatOfferPreferenceOption;


                    foreach(OltppreferenceWithOptionView item in query)
                    {
                        flatOfferPreferenceOption = new FlatOfferPreferenceWithOptionData
                        {
                            PreferenceId = item.PreferenceId,
                            OfferId = item.OfferId,
                            PreferenceName = item.PreferenceName,
                            PreferenceHint = item.PreferenceHint,
                            PreferenceInputType = item.PreferenceInputType,
                            PreferenceMinOptionsSelected = item.PreferenceMinOptionsSelected,
                            PreferenceMaxOptionsSelected = item.PreferenceMaxOptionsSelected,
                            PreferenceIsMandatory = item.PreferenceIsMandatory,
                            PreferenceIsActive = item.PreferenceIsActive,
                            PreferenceCreatedDate = item.PreferenceCreatedDate,
                            PreferenceUpdatedDate = item.PreferenceUpdatedDate,
                            OptionId = item.OptionId,
                            OptionValue = item.OptionValue,
                            OptionPrice = item.OptionPrice,
                            OptionRegularPrice = item.OptionRegularPrice,
                            OptionImgId = item.OptionImgId,
                            OptionImgReplacesOfferOnSelect = item.OptionImgReplacesOfferOnSelect,
                            OptionIsActive = item.OptionIsActive,
                            OptionCreatedDate = item.OptionCreatedDate,
                            OptionUpdatedDate = item.OptionUpdatedDate,
                        };

                        flatOfferPreferenceOptions.Add(flatOfferPreferenceOption);
                    }

                    if (flatOfferPreferenceOptions?.Count > 0)
                    {
                        IEnumerable<IGrouping<Guid, FlatOfferPreferenceWithOptionData>> groupedByPreferenceId = flatOfferPreferenceOptions.GroupBy(x => x.PreferenceId);

                        FlatOfferPreferenceWithOptionData[] preferenceOptionsGroup = null;

                        preferencesWithOptions = new List<OfferPreferenceWithOptions>();
                        OfferPreferenceWithOptions currentPreferenceWithOptions;

                        int count;

                        foreach (IGrouping<Guid, FlatOfferPreferenceWithOptionData> preferenceOptionsDataGroup in groupedByPreferenceId)
                        {
                            preferenceOptionsGroup = preferenceOptionsDataGroup.ToArray();

                            currentPreferenceWithOptions = new OfferPreferenceWithOptions
                            {
                                Id = preferenceOptionsGroup[0].PreferenceId,
                                OfferId = preferenceOptionsGroup[0].OfferId,
                                Name = preferenceOptionsGroup[0].PreferenceName,
                                Hint = preferenceOptionsGroup[0].PreferenceHint,
                                InputType = preferenceOptionsGroup[0].PreferenceInputType,
                                InputTypeName = GetInputTypeName(preferenceOptionsGroup[0].PreferenceInputType),
                                MinOptionsSelected = preferenceOptionsGroup[0].PreferenceMinOptionsSelected,
                                MaxOptionsSelected = preferenceOptionsGroup[0].PreferenceMaxOptionsSelected,
                                Mandatory = preferenceOptionsGroup[0].PreferenceIsMandatory,
                                IsActive = preferenceOptionsGroup[0].PreferenceIsActive,
                                CreatedDate = preferenceOptionsGroup[0].PreferenceCreatedDate,
                                UpdatedDate = preferenceOptionsGroup[0].PreferenceUpdatedDate,
                                Options = new List<PreferenceOptionJoinView>()
                            };

                            count = preferenceOptionsGroup.Count();

                            for (int i = 0; i < count; ++i)
                            {
                                currentPreferenceWithOptions.Options.Add(new PreferenceOptionJoinView
                                {
                                    Id = preferenceOptionsGroup[i].OptionId,
                                    PreferenceId = preferenceOptionsGroup[i].PreferenceId,
                                    OfferId = preferenceOptionsGroup[i].OfferId,
                                    Value = preferenceOptionsGroup[i].OptionValue,
                                    Price = preferenceOptionsGroup[i].OptionPrice ?? -1,
                                    RegularPrice = preferenceOptionsGroup[i].OptionRegularPrice,
                                    ImageId = preferenceOptionsGroup[i].OptionImgId,
                                    ReplacesOfferImgOnSelect = preferenceOptionsGroup[i].OptionImgReplacesOfferOnSelect,
                                    IsActive = preferenceOptionsGroup[i].OptionIsActive,
                                    CreatedDate = preferenceOptionsGroup[i].OptionCreatedDate,
                                    UpdatedDate = preferenceOptionsGroup[i].OptionUpdatedDate
                                });
                            }

                            preferencesWithOptions.Add(currentPreferenceWithOptions);
                        }

                    }
                }
            }
            catch(Exception e)
            {
                preferencesWithOptions = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }


            return preferencesWithOptions;
        }

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new OfferPreferenceManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public OfferPreferenceManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD FILE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
