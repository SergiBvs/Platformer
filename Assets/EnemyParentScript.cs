using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParentScript : MonoBehaviour {

    ///////ESTE SCRIPT LO TENDRAN TODOS LOS PADRES DE LOS ENEMIGOS.
    ///NECESARIO PARA ELIMINARLOS COMPLETAMENTE Y QUE NO SE QUEDE EL PADRE EN ESCENA.

    public void DestroyParent()
    {
        Destroy(this.gameObject);
    }
}
