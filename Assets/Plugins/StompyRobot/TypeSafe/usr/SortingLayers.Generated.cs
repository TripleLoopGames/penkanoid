// ------------------------------------------------------------------------------
//  _______   _____ ___ ___   _   ___ ___ 
// |_   _\ \ / / _ \ __/ __| /_\ | __| __|
//   | |  \ V /|  _/ _|\__ \/ _ \| _|| _| 
//   |_|   |_| |_| |___|___/_/ \_\_| |___|
// 
// This file has been generated automatically by TypeSafe.
// Any changes to this file may be lost when it is regenerated.
// https://www.stompyrobot.uk/tools/typesafe
// 
// TypeSafe Version: 1.2.2-Unity5
// 
// ------------------------------------------------------------------------------



public sealed class SRSortingLayers {
    
    private SRSortingLayers() {
    }
    
    private const string _tsInternal = "1.2.2-Unity5";
    
    public static global::TypeSafe.SortingLayer Background {
        get {
            return __all[0];
        }
    }
    
    public static global::TypeSafe.SortingLayer Default {
        get {
            return __all[1];
        }
    }
    
    public static global::TypeSafe.SortingLayer Ball {
        get {
            return __all[2];
        }
    }
    
    public static global::TypeSafe.SortingLayer Pickups {
        get {
            return __all[3];
        }
    }
    
    public static global::TypeSafe.SortingLayer UI {
        get {
            return __all[4];
        }
    }
    
    private static global::System.Collections.Generic.IList<global::TypeSafe.SortingLayer> __all = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.SortingLayer>(new global::TypeSafe.SortingLayer[] {
                new global::TypeSafe.SortingLayer("Background", -1752030837),
                new global::TypeSafe.SortingLayer("Default", 0),
                new global::TypeSafe.SortingLayer("Ball", 1597482303),
                new global::TypeSafe.SortingLayer("Pickups", -1403419761),
                new global::TypeSafe.SortingLayer("UI", 1522321345)});
    
    public static global::System.Collections.Generic.IList<global::TypeSafe.SortingLayer> All {
        get {
            return __all;
        }
    }
}
