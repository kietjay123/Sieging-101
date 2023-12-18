using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Units;

public class selected_dictionary : MonoBehaviour
{
    static public Dictionary<int, GameObject> selectedTable = new Dictionary<int, GameObject>();

    public void addSelected(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!(selectedTable.ContainsKey(id)))
        {
            selectedTable.Add(id, go);
            go.AddComponent<selection_component>();
            //Debug.Log("Added " + id + " to selected dict");
        }
    }

    public void deselect(int id)
    {
        Destroy(selectedTable[id].GetComponent<selection_component>());
        selectedTable.Remove(id);
        //Debug.Log("removed" + id + " to selected dict");
    }

    public void deselectAll()
    {
        foreach(KeyValuePair<int,GameObject> pair in selectedTable)
        {
            if(pair.Value != null)
            {
                Destroy(selectedTable[pair.Key].GetComponent<selection_component>());
                //Debug.Log("clear");
            }
        }
        selectedTable.Clear();
    }
}
