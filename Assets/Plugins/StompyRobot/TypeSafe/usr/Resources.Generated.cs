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



public sealed class SRResources {
    
    private SRResources() {
    }
    
    private const string _tsInternal = "1.2.2-Unity5";
    
    public static global::TypeSafe.PrefabResource EventSystem {
        get {
            return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[0]));
        }
    }
    
    private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_resources = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.IResource>(new global::TypeSafe.IResource[] {
                new global::TypeSafe.PrefabResource("EventSystem", "EventSystem")});
    
    public sealed class Game {
        
        private Game() {
        }
        
        public static global::TypeSafe.PrefabResource TransitionHole {
            get {
                return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[0]));
            }
        }
        
        public static global::TypeSafe.PrefabResource Canvas_Transition {
            get {
                return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[1]));
            }
        }
        
        public static global::TypeSafe.PrefabResource Main_Camera {
            get {
                return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[2]));
            }
        }
        
        public static global::TypeSafe.PrefabResource BallPool {
            get {
                return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[3]));
            }
        }
        
        public static global::TypeSafe.PrefabResource Block {
            get {
                return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[4]));
            }
        }
        
        public static global::TypeSafe.PrefabResource Scenario {
            get {
                return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[5]));
            }
        }
        
        public static global::TypeSafe.PrefabResource Player {
            get {
                return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[6]));
            }
        }
        
        public static global::TypeSafe.PrefabResource Ball {
            get {
                return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[7]));
            }
        }
        
        private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_resources = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.IResource>(new global::TypeSafe.IResource[] {
                    new global::TypeSafe.PrefabResource("TransitionHole", "Game/TransitionHole"),
                    new global::TypeSafe.PrefabResource("Canvas_Transition", "Game/Canvas_Transition"),
                    new global::TypeSafe.PrefabResource("Main Camera", "Game/Main Camera"),
                    new global::TypeSafe.PrefabResource("BallPool", "Game/BallPool"),
                    new global::TypeSafe.PrefabResource("Block", "Game/Block"),
                    new global::TypeSafe.PrefabResource("Scenario", "Game/Scenario"),
                    new global::TypeSafe.PrefabResource("Player", "Game/Player"),
                    new global::TypeSafe.PrefabResource("Ball", "Game/Ball")});
        
        public sealed class Pickups {
            
            private Pickups() {
            }
            
            public static global::TypeSafe.PrefabResource Heart {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[0]));
                }
            }
            
            public static global::TypeSafe.PrefabResource Clock {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[1]));
                }
            }
            
            public static global::TypeSafe.PrefabResource Star {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[2]));
                }
            }
            
            public static global::TypeSafe.PrefabResource EmptyPickup {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[3]));
                }
            }
            
            private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_resources = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.IResource>(new global::TypeSafe.IResource[] {
                        new global::TypeSafe.PrefabResource("Heart", "Game/Pickups/Heart"),
                        new global::TypeSafe.PrefabResource("Clock", "Game/Pickups/Clock"),
                        new global::TypeSafe.PrefabResource("Star", "Game/Pickups/Star"),
                        new global::TypeSafe.PrefabResource("EmptyPickup", "Game/Pickups/EmptyPickup")});
            
            /// <summary>
            /// Return a list of all resources in this folder.
            /// This method has a very low performance cost, no need to cache the result.
            /// </summary>
            /// <returns>A list of resource objects in this folder.</returns>
            public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContents() {
                return __ts_internal_resources;
            }
            
            private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_recursiveLookupCache;
            
            /// <summary>
            /// Return a list of all resources in this folder and all sub-folders.
            /// The result of this method is cached, so subsequent calls will have very low performance cost.
            /// </summary>
            /// <returns>A list of resource objects in this folder and sub-folders.</returns>
            public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContentsRecursive() {
                if ((__ts_internal_recursiveLookupCache != null)) {
                    return __ts_internal_recursiveLookupCache;
                }
                global::System.Collections.Generic.List<global::TypeSafe.IResource> tmp = new global::System.Collections.Generic.List<global::TypeSafe.IResource>();
                tmp.AddRange(GetContents());
                __ts_internal_recursiveLookupCache = tmp;
                return __ts_internal_recursiveLookupCache;
            }
            
            /// <summary>
            /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref> (does not include sub-folders)
            /// This method does not cache the result, so you should cache the result yourself if you will use it often.
            /// </summary>
            /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder.</returns>
            public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContents<TResource>()
                where TResource : global::UnityEngine.Object {
                return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContents());
            }
            
            /// <summary>
            /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref>, including sub-folders.
            /// This method does not cache the result, so you should cache the result yourself if you will use it often.
            /// </summary>
            /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder and sub-folders.</returns>
            public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContentsRecursive<TResource>()
                where TResource : global::UnityEngine.Object {
                return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContentsRecursive());
            }
            
            /// <summary>
            /// Call Unload() on every loaded resource in this folder.
            /// </summary>
            public static void UnloadAll() {
                global::TypeSafe.TypeSafeUtil.UnloadAll(GetContents());
            }
            
            /// <summary>
            /// Call Unload() on every loaded resource in this folder and subfolders.
            /// </summary>
            private void UnloadAllRecursive() {
                global::TypeSafe.TypeSafeUtil.UnloadAll(GetContentsRecursive());
            }
        }
        
        public sealed class Ui {
            
            private Ui() {
            }
            
            public static global::TypeSafe.PrefabResource EventSystem {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[0]));
                }
            }
            
            public static global::TypeSafe.PrefabResource Canvas {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[1]));
                }
            }
            
            public static global::TypeSafe.PrefabResource EndGame {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[2]));
                }
            }
            
            public static global::TypeSafe.PrefabResource WinGame {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[3]));
                }
            }
            
            public static global::TypeSafe.PrefabResource Health {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[4]));
                }
            }
            
            public static global::TypeSafe.PrefabResource Time {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[5]));
                }
            }
            
            private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_resources = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.IResource>(new global::TypeSafe.IResource[] {
                        new global::TypeSafe.PrefabResource("EventSystem", "Game/Ui/EventSystem"),
                        new global::TypeSafe.PrefabResource("Canvas", "Game/Ui/Canvas"),
                        new global::TypeSafe.PrefabResource("EndGame", "Game/Ui/EndGame"),
                        new global::TypeSafe.PrefabResource("WinGame", "Game/Ui/WinGame"),
                        new global::TypeSafe.PrefabResource("Health", "Game/Ui/Health"),
                        new global::TypeSafe.PrefabResource("Time", "Game/Ui/Time")});
            
            /// <summary>
            /// Return a list of all resources in this folder.
            /// This method has a very low performance cost, no need to cache the result.
            /// </summary>
            /// <returns>A list of resource objects in this folder.</returns>
            public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContents() {
                return __ts_internal_resources;
            }
            
            private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_recursiveLookupCache;
            
            /// <summary>
            /// Return a list of all resources in this folder and all sub-folders.
            /// The result of this method is cached, so subsequent calls will have very low performance cost.
            /// </summary>
            /// <returns>A list of resource objects in this folder and sub-folders.</returns>
            public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContentsRecursive() {
                if ((__ts_internal_recursiveLookupCache != null)) {
                    return __ts_internal_recursiveLookupCache;
                }
                global::System.Collections.Generic.List<global::TypeSafe.IResource> tmp = new global::System.Collections.Generic.List<global::TypeSafe.IResource>();
                tmp.AddRange(GetContents());
                __ts_internal_recursiveLookupCache = tmp;
                return __ts_internal_recursiveLookupCache;
            }
            
            /// <summary>
            /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref> (does not include sub-folders)
            /// This method does not cache the result, so you should cache the result yourself if you will use it often.
            /// </summary>
            /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder.</returns>
            public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContents<TResource>()
                where TResource : global::UnityEngine.Object {
                return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContents());
            }
            
            /// <summary>
            /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref>, including sub-folders.
            /// This method does not cache the result, so you should cache the result yourself if you will use it often.
            /// </summary>
            /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder and sub-folders.</returns>
            public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContentsRecursive<TResource>()
                where TResource : global::UnityEngine.Object {
                return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContentsRecursive());
            }
            
            /// <summary>
            /// Call Unload() on every loaded resource in this folder.
            /// </summary>
            public static void UnloadAll() {
                global::TypeSafe.TypeSafeUtil.UnloadAll(GetContents());
            }
            
            /// <summary>
            /// Call Unload() on every loaded resource in this folder and subfolders.
            /// </summary>
            private void UnloadAllRecursive() {
                global::TypeSafe.TypeSafeUtil.UnloadAll(GetContentsRecursive());
            }
        }
        
        public sealed class Levels {
            
            private Levels() {
            }
            
            public static global::TypeSafe.PrefabResource Level_3 {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[0]));
                }
            }
            
            public static global::TypeSafe.PrefabResource Level_1 {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[1]));
                }
            }
            
            public static global::TypeSafe.PrefabResource Level_2 {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[2]));
                }
            }
            
            private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_resources = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.IResource>(new global::TypeSafe.IResource[] {
                        new global::TypeSafe.PrefabResource("Level_3", "Game/Levels/Level_3"),
                        new global::TypeSafe.PrefabResource("Level_1", "Game/Levels/Level_1"),
                        new global::TypeSafe.PrefabResource("Level_2", "Game/Levels/Level_2")});
            
            /// <summary>
            /// Return a list of all resources in this folder.
            /// This method has a very low performance cost, no need to cache the result.
            /// </summary>
            /// <returns>A list of resource objects in this folder.</returns>
            public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContents() {
                return __ts_internal_resources;
            }
            
            private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_recursiveLookupCache;
            
            /// <summary>
            /// Return a list of all resources in this folder and all sub-folders.
            /// The result of this method is cached, so subsequent calls will have very low performance cost.
            /// </summary>
            /// <returns>A list of resource objects in this folder and sub-folders.</returns>
            public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContentsRecursive() {
                if ((__ts_internal_recursiveLookupCache != null)) {
                    return __ts_internal_recursiveLookupCache;
                }
                global::System.Collections.Generic.List<global::TypeSafe.IResource> tmp = new global::System.Collections.Generic.List<global::TypeSafe.IResource>();
                tmp.AddRange(GetContents());
                __ts_internal_recursiveLookupCache = tmp;
                return __ts_internal_recursiveLookupCache;
            }
            
            /// <summary>
            /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref> (does not include sub-folders)
            /// This method does not cache the result, so you should cache the result yourself if you will use it often.
            /// </summary>
            /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder.</returns>
            public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContents<TResource>()
                where TResource : global::UnityEngine.Object {
                return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContents());
            }
            
            /// <summary>
            /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref>, including sub-folders.
            /// This method does not cache the result, so you should cache the result yourself if you will use it often.
            /// </summary>
            /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder and sub-folders.</returns>
            public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContentsRecursive<TResource>()
                where TResource : global::UnityEngine.Object {
                return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContentsRecursive());
            }
            
            /// <summary>
            /// Call Unload() on every loaded resource in this folder.
            /// </summary>
            public static void UnloadAll() {
                global::TypeSafe.TypeSafeUtil.UnloadAll(GetContents());
            }
            
            /// <summary>
            /// Call Unload() on every loaded resource in this folder and subfolders.
            /// </summary>
            private void UnloadAllRecursive() {
                global::TypeSafe.TypeSafeUtil.UnloadAll(GetContentsRecursive());
            }
        }
        
        /// <summary>
        /// Return a list of all resources in this folder.
        /// This method has a very low performance cost, no need to cache the result.
        /// </summary>
        /// <returns>A list of resource objects in this folder.</returns>
        public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContents() {
            return __ts_internal_resources;
        }
        
        private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_recursiveLookupCache;
        
        /// <summary>
        /// Return a list of all resources in this folder and all sub-folders.
        /// The result of this method is cached, so subsequent calls will have very low performance cost.
        /// </summary>
        /// <returns>A list of resource objects in this folder and sub-folders.</returns>
        public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContentsRecursive() {
            if ((__ts_internal_recursiveLookupCache != null)) {
                return __ts_internal_recursiveLookupCache;
            }
            global::System.Collections.Generic.List<global::TypeSafe.IResource> tmp = new global::System.Collections.Generic.List<global::TypeSafe.IResource>();
            tmp.AddRange(GetContents());
            tmp.AddRange(Pickups.GetContentsRecursive());
            tmp.AddRange(Ui.GetContentsRecursive());
            tmp.AddRange(Levels.GetContentsRecursive());
            __ts_internal_recursiveLookupCache = tmp;
            return __ts_internal_recursiveLookupCache;
        }
        
        /// <summary>
        /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref> (does not include sub-folders)
        /// This method does not cache the result, so you should cache the result yourself if you will use it often.
        /// </summary>
        /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder.</returns>
        public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContents<TResource>()
            where TResource : global::UnityEngine.Object {
            return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContents());
        }
        
        /// <summary>
        /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref>, including sub-folders.
        /// This method does not cache the result, so you should cache the result yourself if you will use it often.
        /// </summary>
        /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder and sub-folders.</returns>
        public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContentsRecursive<TResource>()
            where TResource : global::UnityEngine.Object {
            return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContentsRecursive());
        }
        
        /// <summary>
        /// Call Unload() on every loaded resource in this folder.
        /// </summary>
        public static void UnloadAll() {
            global::TypeSafe.TypeSafeUtil.UnloadAll(GetContents());
        }
        
        /// <summary>
        /// Call Unload() on every loaded resource in this folder and subfolders.
        /// </summary>
        private void UnloadAllRecursive() {
            global::TypeSafe.TypeSafeUtil.UnloadAll(GetContentsRecursive());
        }
    }
    
    public sealed class Menu {
        
        private Menu() {
        }
        
        public static global::TypeSafe.PrefabResource StartBlock {
            get {
                return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[0]));
            }
        }
        
        public static global::TypeSafe.PrefabResource MainMenu {
            get {
                return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[1]));
            }
        }
        
        public static global::TypeSafe.PrefabResource Main_Camera {
            get {
                return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[2]));
            }
        }
        
        public static global::TypeSafe.PrefabResource Scenario {
            get {
                return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[3]));
            }
        }
        
        private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_resources = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.IResource>(new global::TypeSafe.IResource[] {
                    new global::TypeSafe.PrefabResource("StartBlock", "Menu/StartBlock"),
                    new global::TypeSafe.PrefabResource("MainMenu", "Menu/MainMenu"),
                    new global::TypeSafe.PrefabResource("Main Camera", "Menu/Main Camera"),
                    new global::TypeSafe.PrefabResource("Scenario", "Menu/Scenario")});
        
        public sealed class Ui {
            
            private Ui() {
            }
            
            public static global::TypeSafe.PrefabResource EventSystem {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[0]));
                }
            }
            
            public static global::TypeSafe.PrefabResource Background {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[1]));
                }
            }
            
            public static global::TypeSafe.PrefabResource Canvas {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[2]));
                }
            }
            
            public static global::TypeSafe.PrefabResource Title {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[3]));
                }
            }
            
            public static global::TypeSafe.PrefabResource MainPanel {
                get {
                    return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[4]));
                }
            }
            
            private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_resources = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.IResource>(new global::TypeSafe.IResource[] {
                        new global::TypeSafe.PrefabResource("EventSystem", "Menu/Ui/EventSystem"),
                        new global::TypeSafe.PrefabResource("Background", "Menu/Ui/Background"),
                        new global::TypeSafe.PrefabResource("Canvas", "Menu/Ui/Canvas"),
                        new global::TypeSafe.PrefabResource("Title", "Menu/Ui/Title"),
                        new global::TypeSafe.PrefabResource("MainPanel", "Menu/Ui/MainPanel")});
            
            /// <summary>
            /// Return a list of all resources in this folder.
            /// This method has a very low performance cost, no need to cache the result.
            /// </summary>
            /// <returns>A list of resource objects in this folder.</returns>
            public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContents() {
                return __ts_internal_resources;
            }
            
            private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_recursiveLookupCache;
            
            /// <summary>
            /// Return a list of all resources in this folder and all sub-folders.
            /// The result of this method is cached, so subsequent calls will have very low performance cost.
            /// </summary>
            /// <returns>A list of resource objects in this folder and sub-folders.</returns>
            public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContentsRecursive() {
                if ((__ts_internal_recursiveLookupCache != null)) {
                    return __ts_internal_recursiveLookupCache;
                }
                global::System.Collections.Generic.List<global::TypeSafe.IResource> tmp = new global::System.Collections.Generic.List<global::TypeSafe.IResource>();
                tmp.AddRange(GetContents());
                __ts_internal_recursiveLookupCache = tmp;
                return __ts_internal_recursiveLookupCache;
            }
            
            /// <summary>
            /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref> (does not include sub-folders)
            /// This method does not cache the result, so you should cache the result yourself if you will use it often.
            /// </summary>
            /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder.</returns>
            public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContents<TResource>()
                where TResource : global::UnityEngine.Object {
                return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContents());
            }
            
            /// <summary>
            /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref>, including sub-folders.
            /// This method does not cache the result, so you should cache the result yourself if you will use it often.
            /// </summary>
            /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder and sub-folders.</returns>
            public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContentsRecursive<TResource>()
                where TResource : global::UnityEngine.Object {
                return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContentsRecursive());
            }
            
            /// <summary>
            /// Call Unload() on every loaded resource in this folder.
            /// </summary>
            public static void UnloadAll() {
                global::TypeSafe.TypeSafeUtil.UnloadAll(GetContents());
            }
            
            /// <summary>
            /// Call Unload() on every loaded resource in this folder and subfolders.
            /// </summary>
            private void UnloadAllRecursive() {
                global::TypeSafe.TypeSafeUtil.UnloadAll(GetContentsRecursive());
            }
        }
        
        /// <summary>
        /// Return a list of all resources in this folder.
        /// This method has a very low performance cost, no need to cache the result.
        /// </summary>
        /// <returns>A list of resource objects in this folder.</returns>
        public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContents() {
            return __ts_internal_resources;
        }
        
        private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_recursiveLookupCache;
        
        /// <summary>
        /// Return a list of all resources in this folder and all sub-folders.
        /// The result of this method is cached, so subsequent calls will have very low performance cost.
        /// </summary>
        /// <returns>A list of resource objects in this folder and sub-folders.</returns>
        public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContentsRecursive() {
            if ((__ts_internal_recursiveLookupCache != null)) {
                return __ts_internal_recursiveLookupCache;
            }
            global::System.Collections.Generic.List<global::TypeSafe.IResource> tmp = new global::System.Collections.Generic.List<global::TypeSafe.IResource>();
            tmp.AddRange(GetContents());
            tmp.AddRange(Ui.GetContentsRecursive());
            __ts_internal_recursiveLookupCache = tmp;
            return __ts_internal_recursiveLookupCache;
        }
        
        /// <summary>
        /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref> (does not include sub-folders)
        /// This method does not cache the result, so you should cache the result yourself if you will use it often.
        /// </summary>
        /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder.</returns>
        public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContents<TResource>()
            where TResource : global::UnityEngine.Object {
            return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContents());
        }
        
        /// <summary>
        /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref>, including sub-folders.
        /// This method does not cache the result, so you should cache the result yourself if you will use it often.
        /// </summary>
        /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder and sub-folders.</returns>
        public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContentsRecursive<TResource>()
            where TResource : global::UnityEngine.Object {
            return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContentsRecursive());
        }
        
        /// <summary>
        /// Call Unload() on every loaded resource in this folder.
        /// </summary>
        public static void UnloadAll() {
            global::TypeSafe.TypeSafeUtil.UnloadAll(GetContents());
        }
        
        /// <summary>
        /// Call Unload() on every loaded resource in this folder and subfolders.
        /// </summary>
        private void UnloadAllRecursive() {
            global::TypeSafe.TypeSafeUtil.UnloadAll(GetContentsRecursive());
        }
    }
    
    public sealed class SRDebugger {
        
        private SRDebugger() {
        }
        
        public static global::TypeSafe.Resource<global::SRDebugger.Settings> Settings {
            get {
                return ((global::TypeSafe.Resource<global::SRDebugger.Settings>)(__ts_internal_resources[0]));
            }
        }
        
        private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_resources = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.IResource>(new global::TypeSafe.IResource[] {
                    new global::TypeSafe.Resource<global::SRDebugger.Settings>("Settings", "SRDebugger/Settings")});
        
        public sealed class UI {
            
            private UI() {
            }
            
            private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_resources = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.IResource>(new global::TypeSafe.IResource[0]);
            
            public sealed class Prefabs {
                
                private Prefabs() {
                }
                
                public static global::TypeSafe.PrefabResource BugReportPopover {
                    get {
                        return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[0]));
                    }
                }
                
                public static global::TypeSafe.PrefabResource DebugPanel {
                    get {
                        return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[1]));
                    }
                }
                
                public static global::TypeSafe.PrefabResource PinEntry {
                    get {
                        return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[2]));
                    }
                }
                
                public static global::TypeSafe.PrefabResource DockConsole {
                    get {
                        return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[3]));
                    }
                }
                
                public static global::TypeSafe.PrefabResource PinnedUI {
                    get {
                        return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[4]));
                    }
                }
                
                public static global::TypeSafe.PrefabResource BugReportSheet {
                    get {
                        return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[5]));
                    }
                }
                
                public static global::TypeSafe.PrefabResource Trigger {
                    get {
                        return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[6]));
                    }
                }
                
                private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_resources = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.IResource>(new global::TypeSafe.IResource[] {
                            new global::TypeSafe.PrefabResource("BugReportPopover", "SRDebugger/UI/Prefabs/BugReportPopover"),
                            new global::TypeSafe.PrefabResource("DebugPanel", "SRDebugger/UI/Prefabs/DebugPanel"),
                            new global::TypeSafe.PrefabResource("PinEntry", "SRDebugger/UI/Prefabs/PinEntry"),
                            new global::TypeSafe.PrefabResource("DockConsole", "SRDebugger/UI/Prefabs/DockConsole"),
                            new global::TypeSafe.PrefabResource("PinnedUI", "SRDebugger/UI/Prefabs/PinnedUI"),
                            new global::TypeSafe.PrefabResource("BugReportSheet", "SRDebugger/UI/Prefabs/BugReportSheet"),
                            new global::TypeSafe.PrefabResource("Trigger", "SRDebugger/UI/Prefabs/Trigger")});
                
                public sealed class Options {
                    
                    private Options() {
                    }
                    
                    public static global::TypeSafe.PrefabResource BoolOption {
                        get {
                            return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[0]));
                        }
                    }
                    
                    public static global::TypeSafe.PrefabResource ActionOption {
                        get {
                            return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[1]));
                        }
                    }
                    
                    public static global::TypeSafe.PrefabResource ReadOnlyOption {
                        get {
                            return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[2]));
                        }
                    }
                    
                    public static global::TypeSafe.PrefabResource StringOption {
                        get {
                            return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[3]));
                        }
                    }
                    
                    public static global::TypeSafe.PrefabResource EnumOption {
                        get {
                            return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[4]));
                        }
                    }
                    
                    public static global::TypeSafe.PrefabResource NumberOption {
                        get {
                            return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[5]));
                        }
                    }
                    
                    private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_resources = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.IResource>(new global::TypeSafe.IResource[] {
                                new global::TypeSafe.PrefabResource("BoolOption", "SRDebugger/UI/Prefabs/Options/BoolOption"),
                                new global::TypeSafe.PrefabResource("ActionOption", "SRDebugger/UI/Prefabs/Options/ActionOption"),
                                new global::TypeSafe.PrefabResource("ReadOnlyOption", "SRDebugger/UI/Prefabs/Options/ReadOnlyOption"),
                                new global::TypeSafe.PrefabResource("StringOption", "SRDebugger/UI/Prefabs/Options/StringOption"),
                                new global::TypeSafe.PrefabResource("EnumOption", "SRDebugger/UI/Prefabs/Options/EnumOption"),
                                new global::TypeSafe.PrefabResource("NumberOption", "SRDebugger/UI/Prefabs/Options/NumberOption")});
                    
                    /// <summary>
                    /// Return a list of all resources in this folder.
                    /// This method has a very low performance cost, no need to cache the result.
                    /// </summary>
                    /// <returns>A list of resource objects in this folder.</returns>
                    public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContents() {
                        return __ts_internal_resources;
                    }
                    
                    private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_recursiveLookupCache;
                    
                    /// <summary>
                    /// Return a list of all resources in this folder and all sub-folders.
                    /// The result of this method is cached, so subsequent calls will have very low performance cost.
                    /// </summary>
                    /// <returns>A list of resource objects in this folder and sub-folders.</returns>
                    public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContentsRecursive() {
                        if ((__ts_internal_recursiveLookupCache != null)) {
                            return __ts_internal_recursiveLookupCache;
                        }
                        global::System.Collections.Generic.List<global::TypeSafe.IResource> tmp = new global::System.Collections.Generic.List<global::TypeSafe.IResource>();
                        tmp.AddRange(GetContents());
                        __ts_internal_recursiveLookupCache = tmp;
                        return __ts_internal_recursiveLookupCache;
                    }
                    
                    /// <summary>
                    /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref> (does not include sub-folders)
                    /// This method does not cache the result, so you should cache the result yourself if you will use it often.
                    /// </summary>
                    /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder.</returns>
                    public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContents<TResource>()
                        where TResource : global::UnityEngine.Object {
                        return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContents());
                    }
                    
                    /// <summary>
                    /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref>, including sub-folders.
                    /// This method does not cache the result, so you should cache the result yourself if you will use it often.
                    /// </summary>
                    /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder and sub-folders.</returns>
                    public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContentsRecursive<TResource>()
                        where TResource : global::UnityEngine.Object {
                        return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContentsRecursive());
                    }
                    
                    /// <summary>
                    /// Call Unload() on every loaded resource in this folder.
                    /// </summary>
                    public static void UnloadAll() {
                        global::TypeSafe.TypeSafeUtil.UnloadAll(GetContents());
                    }
                    
                    /// <summary>
                    /// Call Unload() on every loaded resource in this folder and subfolders.
                    /// </summary>
                    private void UnloadAllRecursive() {
                        global::TypeSafe.TypeSafeUtil.UnloadAll(GetContentsRecursive());
                    }
                }
                
                public sealed class Tabs {
                    
                    private Tabs() {
                    }
                    
                    public static global::TypeSafe.PrefabResource Console {
                        get {
                            return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[0]));
                        }
                    }
                    
                    public static global::TypeSafe.PrefabResource BugReporter {
                        get {
                            return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[1]));
                        }
                    }
                    
                    public static global::TypeSafe.PrefabResource System {
                        get {
                            return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[2]));
                        }
                    }
                    
                    public static global::TypeSafe.PrefabResource Profiler {
                        get {
                            return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[3]));
                        }
                    }
                    
                    public static global::TypeSafe.PrefabResource Options {
                        get {
                            return ((global::TypeSafe.PrefabResource)(__ts_internal_resources[4]));
                        }
                    }
                    
                    private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_resources = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::TypeSafe.IResource>(new global::TypeSafe.IResource[] {
                                new global::TypeSafe.PrefabResource("Console", "SRDebugger/UI/Prefabs/Tabs/Console"),
                                new global::TypeSafe.PrefabResource("BugReporter", "SRDebugger/UI/Prefabs/Tabs/BugReporter"),
                                new global::TypeSafe.PrefabResource("System", "SRDebugger/UI/Prefabs/Tabs/System"),
                                new global::TypeSafe.PrefabResource("Profiler", "SRDebugger/UI/Prefabs/Tabs/Profiler"),
                                new global::TypeSafe.PrefabResource("Options", "SRDebugger/UI/Prefabs/Tabs/Options")});
                    
                    /// <summary>
                    /// Return a list of all resources in this folder.
                    /// This method has a very low performance cost, no need to cache the result.
                    /// </summary>
                    /// <returns>A list of resource objects in this folder.</returns>
                    public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContents() {
                        return __ts_internal_resources;
                    }
                    
                    private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_recursiveLookupCache;
                    
                    /// <summary>
                    /// Return a list of all resources in this folder and all sub-folders.
                    /// The result of this method is cached, so subsequent calls will have very low performance cost.
                    /// </summary>
                    /// <returns>A list of resource objects in this folder and sub-folders.</returns>
                    public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContentsRecursive() {
                        if ((__ts_internal_recursiveLookupCache != null)) {
                            return __ts_internal_recursiveLookupCache;
                        }
                        global::System.Collections.Generic.List<global::TypeSafe.IResource> tmp = new global::System.Collections.Generic.List<global::TypeSafe.IResource>();
                        tmp.AddRange(GetContents());
                        __ts_internal_recursiveLookupCache = tmp;
                        return __ts_internal_recursiveLookupCache;
                    }
                    
                    /// <summary>
                    /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref> (does not include sub-folders)
                    /// This method does not cache the result, so you should cache the result yourself if you will use it often.
                    /// </summary>
                    /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder.</returns>
                    public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContents<TResource>()
                        where TResource : global::UnityEngine.Object {
                        return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContents());
                    }
                    
                    /// <summary>
                    /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref>, including sub-folders.
                    /// This method does not cache the result, so you should cache the result yourself if you will use it often.
                    /// </summary>
                    /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder and sub-folders.</returns>
                    public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContentsRecursive<TResource>()
                        where TResource : global::UnityEngine.Object {
                        return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContentsRecursive());
                    }
                    
                    /// <summary>
                    /// Call Unload() on every loaded resource in this folder.
                    /// </summary>
                    public static void UnloadAll() {
                        global::TypeSafe.TypeSafeUtil.UnloadAll(GetContents());
                    }
                    
                    /// <summary>
                    /// Call Unload() on every loaded resource in this folder and subfolders.
                    /// </summary>
                    private void UnloadAllRecursive() {
                        global::TypeSafe.TypeSafeUtil.UnloadAll(GetContentsRecursive());
                    }
                }
                
                /// <summary>
                /// Return a list of all resources in this folder.
                /// This method has a very low performance cost, no need to cache the result.
                /// </summary>
                /// <returns>A list of resource objects in this folder.</returns>
                public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContents() {
                    return __ts_internal_resources;
                }
                
                private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_recursiveLookupCache;
                
                /// <summary>
                /// Return a list of all resources in this folder and all sub-folders.
                /// The result of this method is cached, so subsequent calls will have very low performance cost.
                /// </summary>
                /// <returns>A list of resource objects in this folder and sub-folders.</returns>
                public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContentsRecursive() {
                    if ((__ts_internal_recursiveLookupCache != null)) {
                        return __ts_internal_recursiveLookupCache;
                    }
                    global::System.Collections.Generic.List<global::TypeSafe.IResource> tmp = new global::System.Collections.Generic.List<global::TypeSafe.IResource>();
                    tmp.AddRange(GetContents());
                    tmp.AddRange(Options.GetContentsRecursive());
                    tmp.AddRange(Tabs.GetContentsRecursive());
                    __ts_internal_recursiveLookupCache = tmp;
                    return __ts_internal_recursiveLookupCache;
                }
                
                /// <summary>
                /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref> (does not include sub-folders)
                /// This method does not cache the result, so you should cache the result yourself if you will use it often.
                /// </summary>
                /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder.</returns>
                public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContents<TResource>()
                    where TResource : global::UnityEngine.Object {
                    return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContents());
                }
                
                /// <summary>
                /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref>, including sub-folders.
                /// This method does not cache the result, so you should cache the result yourself if you will use it often.
                /// </summary>
                /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder and sub-folders.</returns>
                public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContentsRecursive<TResource>()
                    where TResource : global::UnityEngine.Object {
                    return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContentsRecursive());
                }
                
                /// <summary>
                /// Call Unload() on every loaded resource in this folder.
                /// </summary>
                public static void UnloadAll() {
                    global::TypeSafe.TypeSafeUtil.UnloadAll(GetContents());
                }
                
                /// <summary>
                /// Call Unload() on every loaded resource in this folder and subfolders.
                /// </summary>
                private void UnloadAllRecursive() {
                    global::TypeSafe.TypeSafeUtil.UnloadAll(GetContentsRecursive());
                }
            }
            
            /// <summary>
            /// Return a list of all resources in this folder.
            /// This method has a very low performance cost, no need to cache the result.
            /// </summary>
            /// <returns>A list of resource objects in this folder.</returns>
            public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContents() {
                return __ts_internal_resources;
            }
            
            private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_recursiveLookupCache;
            
            /// <summary>
            /// Return a list of all resources in this folder and all sub-folders.
            /// The result of this method is cached, so subsequent calls will have very low performance cost.
            /// </summary>
            /// <returns>A list of resource objects in this folder and sub-folders.</returns>
            public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContentsRecursive() {
                if ((__ts_internal_recursiveLookupCache != null)) {
                    return __ts_internal_recursiveLookupCache;
                }
                global::System.Collections.Generic.List<global::TypeSafe.IResource> tmp = new global::System.Collections.Generic.List<global::TypeSafe.IResource>();
                tmp.AddRange(GetContents());
                tmp.AddRange(Prefabs.GetContentsRecursive());
                __ts_internal_recursiveLookupCache = tmp;
                return __ts_internal_recursiveLookupCache;
            }
            
            /// <summary>
            /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref> (does not include sub-folders)
            /// This method does not cache the result, so you should cache the result yourself if you will use it often.
            /// </summary>
            /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder.</returns>
            public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContents<TResource>()
                where TResource : global::UnityEngine.Object {
                return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContents());
            }
            
            /// <summary>
            /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref>, including sub-folders.
            /// This method does not cache the result, so you should cache the result yourself if you will use it often.
            /// </summary>
            /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder and sub-folders.</returns>
            public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContentsRecursive<TResource>()
                where TResource : global::UnityEngine.Object {
                return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContentsRecursive());
            }
            
            /// <summary>
            /// Call Unload() on every loaded resource in this folder.
            /// </summary>
            public static void UnloadAll() {
                global::TypeSafe.TypeSafeUtil.UnloadAll(GetContents());
            }
            
            /// <summary>
            /// Call Unload() on every loaded resource in this folder and subfolders.
            /// </summary>
            private void UnloadAllRecursive() {
                global::TypeSafe.TypeSafeUtil.UnloadAll(GetContentsRecursive());
            }
        }
        
        /// <summary>
        /// Return a list of all resources in this folder.
        /// This method has a very low performance cost, no need to cache the result.
        /// </summary>
        /// <returns>A list of resource objects in this folder.</returns>
        public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContents() {
            return __ts_internal_resources;
        }
        
        private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_recursiveLookupCache;
        
        /// <summary>
        /// Return a list of all resources in this folder and all sub-folders.
        /// The result of this method is cached, so subsequent calls will have very low performance cost.
        /// </summary>
        /// <returns>A list of resource objects in this folder and sub-folders.</returns>
        public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContentsRecursive() {
            if ((__ts_internal_recursiveLookupCache != null)) {
                return __ts_internal_recursiveLookupCache;
            }
            global::System.Collections.Generic.List<global::TypeSafe.IResource> tmp = new global::System.Collections.Generic.List<global::TypeSafe.IResource>();
            tmp.AddRange(GetContents());
            tmp.AddRange(UI.GetContentsRecursive());
            __ts_internal_recursiveLookupCache = tmp;
            return __ts_internal_recursiveLookupCache;
        }
        
        /// <summary>
        /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref> (does not include sub-folders)
        /// This method does not cache the result, so you should cache the result yourself if you will use it often.
        /// </summary>
        /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder.</returns>
        public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContents<TResource>()
            where TResource : global::UnityEngine.Object {
            return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContents());
        }
        
        /// <summary>
        /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref>, including sub-folders.
        /// This method does not cache the result, so you should cache the result yourself if you will use it often.
        /// </summary>
        /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder and sub-folders.</returns>
        public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContentsRecursive<TResource>()
            where TResource : global::UnityEngine.Object {
            return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContentsRecursive());
        }
        
        /// <summary>
        /// Call Unload() on every loaded resource in this folder.
        /// </summary>
        public static void UnloadAll() {
            global::TypeSafe.TypeSafeUtil.UnloadAll(GetContents());
        }
        
        /// <summary>
        /// Call Unload() on every loaded resource in this folder and subfolders.
        /// </summary>
        private void UnloadAllRecursive() {
            global::TypeSafe.TypeSafeUtil.UnloadAll(GetContentsRecursive());
        }
    }
    
    /// <summary>
    /// Return a list of all resources in this folder.
    /// This method has a very low performance cost, no need to cache the result.
    /// </summary>
    /// <returns>A list of resource objects in this folder.</returns>
    public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContents() {
        return __ts_internal_resources;
    }
    
    private static global::System.Collections.Generic.IList<global::TypeSafe.IResource> __ts_internal_recursiveLookupCache;
    
    /// <summary>
    /// Return a list of all resources in this folder and all sub-folders.
    /// The result of this method is cached, so subsequent calls will have very low performance cost.
    /// </summary>
    /// <returns>A list of resource objects in this folder and sub-folders.</returns>
    public static global::System.Collections.Generic.IList<global::TypeSafe.IResource> GetContentsRecursive() {
        if ((__ts_internal_recursiveLookupCache != null)) {
            return __ts_internal_recursiveLookupCache;
        }
        global::System.Collections.Generic.List<global::TypeSafe.IResource> tmp = new global::System.Collections.Generic.List<global::TypeSafe.IResource>();
        tmp.AddRange(GetContents());
        tmp.AddRange(Game.GetContentsRecursive());
        tmp.AddRange(Menu.GetContentsRecursive());
        tmp.AddRange(SRDebugger.GetContentsRecursive());
        __ts_internal_recursiveLookupCache = tmp;
        return __ts_internal_recursiveLookupCache;
    }
    
    /// <summary>
    /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref> (does not include sub-folders)
    /// This method does not cache the result, so you should cache the result yourself if you will use it often.
    /// </summary>
    /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder.</returns>
    public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContents<TResource>()
        where TResource : global::UnityEngine.Object {
        return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContents());
    }
    
    /// <summary>
    /// Return a list of all resources in this folder of type <typeparamref>TResource</typeparamref>, including sub-folders.
    /// This method does not cache the result, so you should cache the result yourself if you will use it often.
    /// </summary>
    /// <returns>A list of <typeparamref>TResource</typeparamref> objects in this folder and sub-folders.</returns>
    public static global::System.Collections.Generic.List<global::TypeSafe.Resource<TResource>> GetContentsRecursive<TResource>()
        where TResource : global::UnityEngine.Object {
        return global::TypeSafe.TypeSafeUtil.GetResourcesOfType<TResource>(GetContentsRecursive());
    }
    
    /// <summary>
    /// Call Unload() on every loaded resource in this folder.
    /// </summary>
    public static void UnloadAll() {
        global::TypeSafe.TypeSafeUtil.UnloadAll(GetContents());
    }
    
    /// <summary>
    /// Call Unload() on every loaded resource in this folder and subfolders.
    /// </summary>
    private void UnloadAllRecursive() {
        global::TypeSafe.TypeSafeUtil.UnloadAll(GetContentsRecursive());
    }
}
