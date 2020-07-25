using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities.Misc.Location;
using YOY.Values;

namespace YOY.DAO.Entities.Manager.Misc
{
    public class StateManager
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

        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS METHODS                                                                                                                                  //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        #region METHODS

        /// <summary>
        /// Retrieves all states
        /// </summary>
        /// <returns></returns>
        public List<State> Gets(Guid countryId, int activeState)
        {
            List<State> states = new List<State>();

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.Active:
                        query = from x in this._businessObjects.Context.DefstatesDataView
                                where (bool)x.IsActive && x.CountryId == countryId
                                orderby x.Name ascending
                                select x;
                        break;
                    case ActiveStates.Inactive:
                        query = from x in this._businessObjects.Context.DefstatesDataView
                                where !(bool)x.IsActive && x.CountryId == countryId
                                orderby x.Name ascending
                                select x;
                        break;
                    case ActiveStates.All:
                        query = from x in this._businessObjects.Context.DefstatesDataView
                                where x.CountryId == countryId
                                orderby x.Name ascending
                                select x;
                        break;
                }
                

                State state = null;
                foreach (DefstatesDataView item in query)
                {
                    state = new State()
                    {
                        Id = item.Id,
                        CountryId = item.CountryId,
                        Code = item.Code,
                        Name = item.Name,
                        CountryFlag = item.CountryFlag,
                        IsActive = item.IsActive,
                        UtcTimeZone = item.UtcTimeZone,
                        InOperation = item.InOperation,
                        NearestStateId = item.NearestStateId,
                        CentralLatitude = item.CenterLatitude,
                        CentralLongitude = item.CenterLongitude
                    };

                    states.Add(state);
                }
            }
            catch (Exception e)
            {
                states = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return states;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// Retrieves all states
        /// </summary>
        /// <returns></returns>
        public List<State> Gets(int activeState)
        {
            List<State> states = new List<State>();

            try
            {
                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.Active:
                        query = from x in this._businessObjects.Context.DefstatesDataView
                                where x.IsActive
                                orderby x.Name ascending
                                select x;
                        break;
                    case ActiveStates.Inactive:
                        query = from x in this._businessObjects.Context.DefstatesDataView
                                where !x.IsActive
                                orderby x.Name ascending
                                select x;
                        break;
                    case ActiveStates.All:
                        query = from x in this._businessObjects.Context.DefstatesDataView
                                orderby x.Name ascending
                                select x;
                        break;
                }
                

                State state = null;
                foreach (DefstatesDataView item in query)
                {
                    state = new State()
                    {
                        Id = item.Id,
                        CountryId = item.CountryId,
                        Code = item.Code,
                        Name = item.Name,
                        CountryFlag = item.CountryFlag,
                        IsActive = item.IsActive,
                        InOperation = item.InOperation,
                        NearestStateId = item.NearestStateId,
                        UtcTimeZone = item.UtcTimeZone,
                        CentralLatitude = item.CenterLatitude,
                        CentralLongitude = item.CenterLongitude
                    };

                    states.Add(state);
                }
            }
            catch (Exception e)
            {
                states = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return states;
        }//GETS ENDS ------------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Retrieve a state
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        public State Get(Guid stateId)
        {
            State state = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefstatesDataView
                            where x.Id == stateId
                            select x;

                foreach (DefstatesDataView item in query)
                {
                    state = new State()
                    {
                        Id = item.Id,
                        CountryId = item.CountryId,
                        Code = item.Code,
                        Name = item.Name,
                        CountryName = item.CountryName,
                        CountryCode = item.CountryCode,
                        CountryFlag = item.CountryFlag,
                        IsActive = item.IsActive,
                        InOperation = item.InOperation,
                        NearestStateId = item.NearestStateId,
                        UtcTimeZone = item.UtcTimeZone,
                        CentralLatitude = item.CenterLatitude,
                        CentralLongitude = item.CenterLongitude
                    };
                }
            }
            catch (Exception e)
            {
                state = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return state;
        }//GET ENDS ------------------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Retrieve a state
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        public State Get(Guid countryId, string stateCode)
        {
            State state = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefstatesDataView
                            where x.CountryId == countryId && x.Code == stateCode
                            select x;

                foreach (DefstatesDataView item in query)
                {
                    state = new State()
                    {
                        Id = item.Id,
                        CountryId = item.CountryId,
                        Code = item.Code,
                        Name = item.Name,
                        CountryFlag = item.CountryFlag,
                        IsActive = item.IsActive,
                        InOperation = item.InOperation,
                        NearestStateId = item.NearestStateId,
                        UtcTimeZone = item.UtcTimeZone,
                        CentralLatitude = item.CenterLatitude,
                        CentralLongitude = item.CenterLongitude
                    };
                }
            }
            catch (Exception e)
            {
                state = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return state;
        }//GET ENDS ------------------------------------------------------------------------------------------------------------------------------------- //


        private List<StatePlainData> GetEnabledStates()
        {
            List<StatePlainData> statesData = null;

            try
            {
                var query = from x in this._businessObjects.FuncsHandler.GetAvailableStates()
                            orderby x.CountryName ascending, x.StateName ascending
                            select x;

                StatePlainData state = null;
                statesData = new List<StatePlainData>();

                foreach (Tempstates item in query)
                {
                    state = new StatePlainData
                    {
                        CountryId = item.CountryId,
                        CountryName = item.CountryName,
                        CountryFlag = item.CountryFlag,
                        StateId = item.StateId,
                        StateName = item.StateName,
                        StateLatitude = item.StateLatitude,
                        StateLongitude = item.StateLongitude
                    };

                    statesData.Add(state);
                }
            }
            catch (Exception e)
            {
                statesData = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return statesData;
        }

        public List<StatesByCountry> GetStatesByCountry()
        {
            List<StatesByCountry> enabledStates = null;

            try
            {
                List<StatePlainData> plainStates = this.GetEnabledStates();

                if (plainStates?.Count > 0)
                {
                    IEnumerable<IGrouping<Guid, StatePlainData>> groupedByCountryId = plainStates.GroupBy(x => x.CountryId);
                    StatesByCountry states = null;
                    StatePlainData[] statesData = null;

                    enabledStates = new List<StatesByCountry>();

                    foreach (IGrouping<Guid, StatePlainData> countryStates in groupedByCountryId)
                    {
                        statesData = countryStates.ToArray();

                        states = new StatesByCountry
                        {
                            Country = new DTO.Entities.Misc.Structure.POCO.Pair<Guid, string> { Key = statesData[0].CountryId, Value = statesData[0].CountryName },
                            States = new List<StateBaseData>(),
                            ContryFlag = statesData[0].CountryFlag
                        };


                        for (int i = 0; i < statesData.Length; ++i)
                        {
                            states.States.Add
                                (
                                    new StateBaseData
                                    {
                                        Id = statesData[i].StateId,
                                        Name = statesData[i].StateName,
                                        Latitude = statesData[i].StateLatitude,
                                        Longitude = statesData[i].StateLongitude
                                    }
                                );
                        }

                        states.States = states.States.OrderBy(o => o.Name).ToList();

                        enabledStates.Add(states);
                    }

                    enabledStates = enabledStates.OrderBy(o => o.Country.Value).ToList();
                }
            }
            catch (Exception e)
            {
                enabledStates = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");
            }

            return enabledStates;
        }

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new FileManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public StateManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException("businessObjects");
            } // ELSE ENDS
        } // METHOD FILE MANAGER ------------------------------------------------------------------------------------------------------------------------ //


        #endregion
    }
}
