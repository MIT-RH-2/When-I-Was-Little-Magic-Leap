using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class IntegerSyncModel
{
    [RealtimeProperty(1, true, true)]
    private int _avatarInd = 0;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class IntegerSyncModel : IModel {
    // Properties
    public int avatarInd {
        get { return _cache.LookForValueInCache(_avatarInd, entry => entry.avatarIndSet, entry => entry.avatarInd); }
        set { if (value == avatarInd) return; _cache.UpdateLocalCache(entry => { entry.avatarIndSet = true; entry.avatarInd = value; return entry; }); FireAvatarIndDidChange(value); }
    }
    
    // Events
    public delegate void AvatarIndDidChange(IntegerSyncModel model, int value);
    public event         AvatarIndDidChange avatarIndDidChange;
    
    // Delta updates
    private struct LocalCacheEntry {
        public bool avatarIndSet;
        public int  avatarInd;
    }
    
    private LocalChangeCache<LocalCacheEntry> _cache;
    
    public IntegerSyncModel() {
        _cache = new LocalChangeCache<LocalCacheEntry>();
    }
    
    // Events
    public void FireAvatarIndDidChange(int value) {
        try {
            if (avatarIndDidChange != null)
                avatarIndDidChange(this, value);
        } catch (System.Exception exception) {
            Debug.LogException(exception);
        }
    }
    
    // Serialization
    enum PropertyID {
        AvatarInd = 1,
    }
    
    public int WriteLength(StreamContext context) {
        int length = 0;
        
        if (context.fullModel) {
            // Mark unreliable properties as clean and flatten the in-flight cache.
            // TODO: Move this out of WriteLength() once we have a prepareToWrite method.
            _avatarInd = avatarInd;
            _cache.Clear();
            
            // Write all properties
            length += WriteStream.WriteVarint32Length((uint)PropertyID.AvatarInd, (uint)_avatarInd);
        } else {
            // Reliable properties
            if (context.reliableChannel) {
                LocalCacheEntry entry = _cache.localCache;
                if (entry.avatarIndSet)
                    length += WriteStream.WriteVarint32Length((uint)PropertyID.AvatarInd, (uint)entry.avatarInd);
            }
        }
        
        return length;
    }
    
    public void Write(WriteStream stream, StreamContext context) {
        if (context.fullModel) {
            // Write all properties
            stream.WriteVarint32((uint)PropertyID.AvatarInd, (uint)_avatarInd);
        } else {
            // Reliable properties
            if (context.reliableChannel) {
                LocalCacheEntry entry = _cache.localCache;
                if (entry.avatarIndSet)
                    _cache.PushLocalCacheToInflight(context.updateID);
                
                if (entry.avatarIndSet)
                    stream.WriteVarint32((uint)PropertyID.AvatarInd, (uint)entry.avatarInd);
            }
        }
    }
    
    public void Read(ReadStream stream, StreamContext context) {
        bool avatarIndExistsInChangeCache = _cache.ValueExistsInCache(entry => entry.avatarIndSet);
        
        // Remove from in-flight
        if (context.deltaUpdatesOnly && context.reliableChannel)
            _cache.RemoveUpdateFromInflight(context.updateID);
        
        // Loop through each property and deserialize
        uint propertyID;
        while (stream.ReadNextPropertyID(out propertyID)) {
            switch (propertyID) {
                case (uint)PropertyID.AvatarInd: {
                    int previousValue = _avatarInd;
                    
                    _avatarInd = (int)stream.ReadVarint32();
                    
                    if (!avatarIndExistsInChangeCache && _avatarInd != previousValue)
                        FireAvatarIndDidChange(_avatarInd);
                    break;
                }
                default:
                    stream.SkipProperty();
                    break;
            }
        }
    }
}
/* ----- End Normal Autogenerated Code ----- */
