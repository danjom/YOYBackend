using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using YOY.UserAPI.Models.v1.Miscellaneous.Location.POCO;

namespace YOY.UserAPI.Logic.Location
{
    public static class LocationProcessor
    {
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //:::                                                                         :::
        //:::  This routine calculates the distance between two points (given the     :::
        //:::  latitude/longitude of those points). It is being used to calculate     :::
        //:::  the distance between two locations using GeoDataSource(TM) products    :::
        //:::                                                                         :::
        //:::  Definitions:                                                           :::
        //:::    South latitudes are negative, east longitudes are positive           :::
        //:::                                                                         :::
        //:::  Passed to function:                                                    :::
        //:::    lat1, lon1 = Latitude and Longitude of point 1 (in decimal degrees)  :::
        //:::    lat2, lon2 = Latitude and Longitude of point 2 (in decimal degrees)  :::
        //:::    unit = the unit you desire for results                               :::
        //:::           where: 'M' is statute miles (default)                         :::
        //:::                  'K' is kilometers                                      :::
        //:::                  'N' is nautical miles                                  :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        public static double CalculateDistance(double startLat, double startLong, double endLat, double endLong, char unit)
        {
            double rStartLat = Math.PI * startLat / 180;
            double rEndLat = Math.PI * endLat / 180;
            double theta = startLong - endLong;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rStartLat) * Math.Sin(rEndLat) + Math.Cos(rStartLat) *
                Math.Cos(rEndLat) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case 'K': //Kilometers -> default
                    dist *= 1.609344;
                    break;
                case 'N': //Nautical Miles 
                    dist *= 0.8684;
                    break;
                case 'M': //Miles
                    break;
            }

            dist = Math.Round(dist, 3);

            return dist;
        }

        public static LocationAddress GetProccessedAddress(XElement address)
        {
            LocationAddress locationAddress = new LocationAddress()
            {
                LocationIds = new List<Guid>(),
                LocationNames = new List<string>()
            };

            string[] processedAddress;
            string[] addressElement;

            try
            {
                processedAddress = address.Elements("Element")
                                    .Select(el => el.Attribute("id").Value)
                                    .ToArray();

                for (int i = 0; i < processedAddress.Length; ++i)
                {
                    //To get the element components
                    addressElement = processedAddress[i].Split(':');

                    if (addressElement != null && addressElement.Length == 2)
                    {
                        //Stores the location Id and Name
                        locationAddress.LocationIds.Add(new Guid(addressElement.ElementAt(1)));
                        locationAddress.LocationNames.Add(addressElement.ElementAt(0));
                    }

                }
            }
            catch (Exception)
            {
                processedAddress = null;
                //TODO ERROR HANDLING
            }


            return locationAddress;
        }

        public static ProcessedLocation ProcessLocation(string location)
        {
            ProcessedLocation processedLocation = new ProcessedLocation()
            {
                ValidLocation = true,
            };

            try
            {
                string[] locationData = location.Trim().Split('*');
                decimal latitude = 0;
                decimal longitude = 0;

                if (locationData != null && locationData.Length == 2)
                {
                    try
                    {

                        //-------------------COORDINATES DATA-------------------------//
                        //Latitude
                        decimal.TryParse(locationData.ElementAt(0), out latitude);
                        //Longitude
                        decimal.TryParse(locationData.ElementAt(1), out longitude);

                        processedLocation.Latitude = latitude;
                        processedLocation.Longitude = longitude;

                        //This means there aren't valid coordinates
                        if (latitude == 0 && longitude == 0)
                            processedLocation.ValidLocation = false;

                        //This are the latitude and longitude limits
                        if (latitude < -90 || latitude > 90 || longitude < -180 || longitude > 180)
                            processedLocation.ValidLocation = false;


                    }
                    catch (Exception)
                    {
                        processedLocation.ValidLocation = false;
                    }

                }
                else
                {
                    processedLocation.ValidLocation = false;
                }

            }
            catch (Exception)
            {
                processedLocation = null;
                //TODO ERROR HANDLING
            }


            if (!processedLocation.ValidLocation)
            {
                processedLocation.Latitude = 0;
                processedLocation.Longitude = 0;
            }


            return processedLocation;
        }

        public static ProcessedClaimLocation ProcessRedemptionLocation(string location)
        {
            ProcessedClaimLocation processedLocation = new ProcessedClaimLocation()
            {
                ValidLocation = false,
            };

            try
            {
                string[] locationData = location.Trim().Split('*');
                double latitude = 0;
                double longitude = 0;

                if (locationData != null && locationData.Length == 3)
                {
                    try
                    {

                        //-------------------COORDINATES DATA-------------------------//
                        //Latitude
                        double.TryParse(locationData.ElementAt(1), out latitude);
                        //Longitude
                        double.TryParse(locationData.ElementAt(2), out longitude);

                        processedLocation.Latitude = latitude;
                        processedLocation.Longitude = longitude;


                        //----------------------SIGNAL DATA----------------------------//
                        processedLocation.SignalId = locationData.ElementAt(0);

                        //If no valid signal
                        if (processedLocation.SignalId == "0")
                        {
                            processedLocation.SignalId = "";
                        }


                        //If at least one of coordinates or signal are valid, then location is valid
                        if ((latitude != 0 && longitude != 0) || !string.IsNullOrWhiteSpace(processedLocation.SignalId))
                            processedLocation.ValidLocation = true;


                    }
                    catch (Exception)
                    {
                        processedLocation.ValidLocation = false;
                    }

                }
                else
                {
                    processedLocation.ValidLocation = false;
                }

            }
            catch (Exception)
            {
                processedLocation = null;
                //TODO ERROR HANDLING
            }


            return processedLocation;
        }
    }
}