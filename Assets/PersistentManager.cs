using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Persistence;
using UnityEngine.VR.WSA;
using UnityEngine.UI;

public class PersistentManager : MonoBehaviour {

    GameObject anchorW, anchorB;
    WorldAnchorStore store;
    TCPNetwork network;
    bool savedRoot;
	// Use this for initialization
	void Start () {
        WorldAnchorStore.GetAsync(StoreLoaded);
        anchorW = GameObject.Find("AnchorWall");
        anchorB = GameObject.Find("AnchorBoundary");
        
        network = GameObject.Find("NetworkManager").GetComponent<TCPNetwork>();
        
    }

    // Update is called once per frame
    void Update()
    {/*
        switch (network.save)
        {
            case 1:
                SaveGame(1);
                network.save = 0;
                break;
            case 2:
                LoadGame(1);
                network.save = 0;
                break;
            case 3:
                SaveGame(2);
                network.save = 0;
                break;
            case 4:
                LoadGame(2);
                network.save = 0;
                break;
            case 5:
                SaveGame(3);
                network.save = 0;
                break;
            case 6:
                LoadGame(3);
                network.save = 0;
                break;
        }*/
    }

    private void SaveGame(int op)
    {

        // Save data about holograms positioned by this world anchor
        
        if (!this.savedRoot) // Only Save the root once
        {
            if (op == 1 || op == 3) this.savedRoot = this.store.Save("anchorWall", anchorW.AddComponent<WorldAnchor>());
            if (op == 2 || op == 3) this.savedRoot = this.store.Save("anchorBoundary", anchorB.AddComponent<WorldAnchor>());
            Debug.Log("Saved :" + this.savedRoot);
        }
        showSaves();
    }

    private void showSaves()
    {
        string[] ids = this.store.GetAllIds();
        for (int index = 0; index < ids.Length; index++)
        {
            Debug.Log(ids[index]);
        }
    }

    private void LoadGame(int op)
    {
        // Save data about holograms positioned by this world anchor
        if (op == 1 || op == 3) this.savedRoot = this.store.Load("anchorWall", anchorW);
        if (op == 2 || op == 3) this.savedRoot = this.store.Load("anchorBoundary", anchorB);
        if (!this.savedRoot)
        {
            // We didn't actually have the game root saved! We have to re-place our objects or start over
        }
        showSaves();
    }

    private void StoreLoaded(WorldAnchorStore store)
    {
        this.store = store;
    }

}
