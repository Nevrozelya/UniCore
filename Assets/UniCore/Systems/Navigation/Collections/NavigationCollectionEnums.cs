namespace UniCore.Systems.Navigation.Collections
{
    public enum NavigationCollectionConduct
    {
        // This enum is here to decide what happens
        // when an already existing scene in given
        // to push/add in a NavigationCollection.

        // There are 2 case :
        // - Forbidden, we just return and don't process the call
        // - Replace, we override the existing scene (bundle included)

        // It would have been possible (Unity allows that) to create
        // a "MultipleAllowed" conduct, to allow multiple instances
        // of a same scene to be loaded simultaneously.
        // For now, it is considered as a bad idea (:

        Forbidden,
        Replace
    }
}
