using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HarbourManagement
{
    /// <summary>
    /// Display all the information Of the dock
    /// </summary>
    class DisplayDockInformation
    {
        private static Dictionary<int, string> harbour = BerthManagement.harbour;
        public static void DisplayInfo(List<Boat> _parkedBoats)
        {
            Console.WriteLine("Dock[N] \t BoatType \t IdentityNumber\t Weight \t MaxSpeed \t  Others \t \t \t  DaysIntheDock");
            Console.WriteLine();
            var boatIds = harbour.Values.Where(x => x != null).Distinct();
            foreach (var id in boatIds)
            {

                if (id.Split(", ").Length == 2)
                {
                    var ids = id.Split(", ");
                    foreach (var _id in ids)
                    {
                        var key = harbour.FirstOrDefault(x => x.Value == id).Key;
                        var boat = _parkedBoats.FirstOrDefault(x => x.IdentityNumber == _id.Trim());
                        if (boat != null)
                        {
                            if (boat is RowingBoat)
                            {
                                Console.WriteLine($"{key}    \t\t {boat.GetType().Name} -- \t {boat.IdentityNumber} -- \t {boat.Weight} kg  -- \t {boat.MaximumSpeed} km/h  -- \t MaximumNumberOfPassengers:{((RowingBoat)boat).MaximumNumberOfPassengers} -- \t {boat.DaysCount} D");
                            }
                        }
                        else if (boat == null)
                        {
                            Console.WriteLine($"{key}    \t\t Empty-- \t --- -- \t --  -- \t --  -- \t -- -- \t ----");

                        }
                    }
                }
                else
                {
                    var keys = harbour.Where(pair => pair.Value == id).Select(pair => pair.Key);
                    var boat = _parkedBoats.FirstOrDefault(x => x.IdentityNumber == id);
                    if (boat != null)
                    {
                        if (boat is RowingBoat)
                        {
                            Console.WriteLine($"{string.Join("-", keys)}   \t\t {boat.GetType().Name} -- \t {boat.IdentityNumber} -- \t {boat.Weight} kg  -- \t {boat.MaximumSpeed} km/h  -- \t NumberOfHorsepower:{((RowingBoat)boat).MaximumNumberOfPassengers} hp -- \t\t {boat.DaysCount} D");
                        }
                        else if (boat is MotorBoat)
                        {
                            Console.WriteLine($"{string.Join("-", keys)}   \t\t {boat.GetType().Name} -- \t {boat.IdentityNumber} -- \t {boat.Weight} kg  -- \t {boat.MaximumSpeed} km/h  -- \t NumberOfHorsepower:{((MotorBoat)boat).NumberOfHorsepower} hp -- \t\t {boat.DaysCount} D");
                        }
                        else if (boat is SailBoat)
                        {
                            Console.WriteLine($"{string.Join("-", keys)}    \t\t {boat.GetType().Name} -- \t {boat.IdentityNumber} -- \t {boat.Weight} kg  -- \t {boat.MaximumSpeed} km/h  -- \t BoatLength:{((SailBoat)boat).BoatLength} --\t\t        {boat.DaysCount} D");
                        }
                        else if (boat is CargoShip)
                        {
                            Console.WriteLine($"{string.Join("-", keys)}  \t {boat.GetType().Name} -- \t {boat.IdentityNumber} -- \t {boat.Weight} kg  -- \t {boat.MaximumSpeed} km/h  -- \t NumberOfContainers:{((CargoShip)boat).NumberOfContainers} -- \t\t {boat.DaysCount} D");
                        }
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("....................................................................................................................................");

            var TotalBoats = _parkedBoats
                            .Where(x => x != null)
                            .GroupBy(t => t.BoatType);

            foreach (var boats in TotalBoats)
            {
                if (boats.Key != null)
                {
                    Console.Write($"{boats.Key}: {boats.Count()}\t");
                }

            }

            var Weights = _parkedBoats.Sum(w => w.Weight);

            Console.Write($"Total Weights:{Weights}\t");

            if (_parkedBoats.Count > 0)
            {
                var AverageSpeed = _parkedBoats.Sum(a => a.MaximumSpeed);
                Console.Write($"AverageSpeed:{AverageSpeed / _parkedBoats.Count}\t ");
            }
            Console.Write($"Total Parking: {harbour.Count}\t");
            Console.Write($"EmptyParking: {BerthManagement.emptyParking}\t");
            Console.Write($"RefusedBoats Today: {BerthManagement.refusedFromDock}\t");
            Console.WriteLine();
            Console.WriteLine("....................................................................................................................................");

        }
        public static void ParkingFullWarning() 
        {
            Console.WriteLine("ParkingFull Come Tomorrow Please.");
        }


    }
}