

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HarbourManagement
{
    /// <summary>
    /// Creat File and read from the file
    /// </summary>
    class FileSystem
    {
        private static List<Boat> _parkedBoats = BerthManagement._parkedBoats;
        private static Dictionary<int, string> harbour = BerthManagement.harbour;
        public static void CreatSpreadsheet()
        {
            string spreadsheetPath = @"C:\Visual Stadio\HarbourManagement\DockedBoatsInHarbor.csv";
            var csv = new StringBuilder();
            for (int i = 0; i < _parkedBoats.Count; i++)
            {
                if (_parkedBoats[i] != null)
                {

                    if (_parkedBoats[i] is RowingBoat)
                    {
                       var  dockedBoat = (_parkedBoats[i].BoatType, _parkedBoats[i].IdentityNumber, _parkedBoats[i].Weight, _parkedBoats[i].MaximumSpeed, _parkedBoats[i].DaysCount, ((RowingBoat)_parkedBoats[i]).MaximumNumberOfPassengers, _parkedBoats[i].ParkingPlace);
                        csv.AppendLine(string.Join(",", dockedBoat));
                    }
                    else if (_parkedBoats[i] is MotorBoat)
                    {
                        var dockedBoat = (_parkedBoats[i].BoatType, _parkedBoats[i].IdentityNumber, _parkedBoats[i].Weight, _parkedBoats[i].MaximumSpeed, _parkedBoats[i].DaysCount, ((MotorBoat)_parkedBoats[i]).NumberOfHorsepower, _parkedBoats[i].ParkingPlace);
                        csv.AppendLine(string.Join(",", dockedBoat));
                    }
                    else if (_parkedBoats[i] is SailBoat)
                    {
                        var dockedBoat = (_parkedBoats[i].BoatType, _parkedBoats[i].IdentityNumber, _parkedBoats[i].Weight, _parkedBoats[i].MaximumSpeed, _parkedBoats[i].DaysCount, ((SailBoat)_parkedBoats[i]).BoatLength, _parkedBoats[i].ParkingPlace);
                        csv.AppendLine(string.Join(",", dockedBoat));
                    }
                    else if (_parkedBoats[i] is CargoShip)
                    {
                        var dockedBoat = (_parkedBoats[i].BoatType, _parkedBoats[i].IdentityNumber, _parkedBoats[i].Weight, _parkedBoats[i].MaximumSpeed, _parkedBoats[i].DaysCount, ((CargoShip)_parkedBoats[i]).NumberOfContainers, _parkedBoats[i].ParkingPlace);
                        csv.AppendLine(string.Join(",", dockedBoat));
                    }
                }
            }
            File.WriteAllText(spreadsheetPath, csv.ToString());
        }
        public static List<Boat> ReadBoatInfoFromFile()
        {
            var boats = new List<Boat>();

            const string filePath = @"C:\Visual Stadio\HarbourManagement\DockedBoatsInHarbor.csv";
            foreach (string boat in File.ReadLines(filePath, System.Text.Encoding.UTF7))
            {
                char[] delimiterChars = { ' ', ',', '(', ')' };
                string[] boatData = boat.Trim().Split(delimiterChars);
                if (boatData[1] == "RowingBoat")
                {
                    var rowingBoat = new RowingBoat(boatData[1], boatData[3],
                        int.Parse(boatData[5]), int.Parse(boatData[7]), int.Parse(boatData[9]), int.Parse(boatData[11]));
                    rowingBoat.ParkingPlace = boatData[13];
                    BerthManagement.emptyParking -= .5;
                    boats.Add(rowingBoat);
                }
                else if (boatData[1] == "MotorBoat")
                {
                    var motorBoat = new MotorBoat(boatData[1], boatData[3],
                                            int.Parse(boatData[5]), int.Parse(boatData[7]), int.Parse(boatData[9]), int.Parse(boatData[11]));
                    motorBoat.ParkingPlace = boatData[13];
                    BerthManagement.emptyParking -= 1;
                    boats.Add(motorBoat);
                }
                else if (boatData[1] == "SailBoat")
                {
                    var sailBoat = new SailBoat(boatData[1], boatData[3],
                                            int.Parse(boatData[5]), int.Parse(boatData[7]), int.Parse(boatData[9]), int.Parse(boatData[11]));
                    sailBoat.ParkingPlace = boatData[13];
                    BerthManagement.emptyParking -= 2;
                    boats.Add(sailBoat);
                }
                else if (boatData[1] == "CargoShip")
                {
                    var cargoShip = new CargoShip(boatData[1], boatData[3],
                                            int.Parse(boatData[5]), int.Parse(boatData[7]), int.Parse(boatData[9]), int.Parse(boatData[11]));
                    cargoShip.ParkingPlace = boatData[13];
                    BerthManagement.emptyParking -= 4;
                    boats.Add(cargoShip);
                }
            }
            return boats;
        }

    }
}
