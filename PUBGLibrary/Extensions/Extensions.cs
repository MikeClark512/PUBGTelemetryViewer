using System;
using System.Collections.Generic;
using System.Text;
namespace PUBGLibrary.API
{
    public static class EnumExtensions
    {
        public static string EnumForString(this VehicleId str)
        {
            switch (str)
            {
                case VehicleId.AquaRailA01_C: return "AquaRail";
                case VehicleId.BpMotorbike04_C:
                case VehicleId.BpMotorbike04_Desert_C: return "Bike";
                case VehicleId.BpMotorbike04_SideCar_C:
                case VehicleId.BpMotorbike04_SideCarDesert_C: return "3 Man Bike";
                case VehicleId.BpPickupTruckA01_C:
                case VehicleId.BpPickupTruckA02_C:
                case VehicleId.BpPickupTruckA03_C:
                case VehicleId.BpPickupTruckA04_C:
                case VehicleId.BpPickupTruckA05_C:
                case VehicleId.BpPickupTruckB01_C:
                case VehicleId.BpPickupTruckB02_C:
                case VehicleId.BpPickupTruckB03_C:
                case VehicleId.BpPickupTruckB04_C:
                case VehicleId.BpPickupTruckB05_C: return "Pickup Truck";
                case VehicleId.BpVanA01_C:
                case VehicleId.BpVanA02_C:
                case VehicleId.BpVanA03_C:
                case VehicleId.BuggyA01_C:
                case VehicleId.BuggyA02_C:
                case VehicleId.BuggyA03_C:
                case VehicleId.BuggyA04_C:
                case VehicleId.BuggyA05_C:
                case VehicleId.BuggyA06_C: return "Buggy";
                case VehicleId.Dacia_A_02_v2_C:
                case VehicleId.Dacia_A_03_v2_C:
                case VehicleId.Dacia_A_04_v2_C: return "Dacia";
                case VehicleId.Uaz_A_01_C:
                case VehicleId.Uaz_B_01_C:
                case VehicleId.Uaz_C_01_C: return "UAZ";
                case VehicleId.DummyTransportAircraft_C: return "Plane";
                case VehicleId.Empty: return "";
                case VehicleId.ParachutePlayer_C: return "Parachute";
                case VehicleId.BoatPG117_C:
                case VehicleId.PG117_A_01_C: return "Boat";
                default: return "";
            }
        }

        
    }
}
