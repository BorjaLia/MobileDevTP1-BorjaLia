using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PalletMover : ManejoPallets
{

    public InputActionReference moveAction;

    public ManejoPallets Desde, Hasta;
    bool segundoCompleto = false;

    Vector2 lastDir; 
    private void Update()
    {
        Vector2 dir = moveAction.action.ReadValue<Vector2>(); 

        if(lastDir != dir && dir != Vector2.zero)
        {
            Debug.Log("dir key: " + dir);
        }

        lastDir = dir;

        if (!Tenencia() && Desde.Tenencia() && dir == Vector2.left)
        {
            PrimerPaso();
        }
        if (Tenencia() && dir == Vector2.down)
        {
            SegundoPaso();
        }
        if (segundoCompleto && Tenencia() && dir == Vector2.right)
        {
            TercerPaso();
        }
    }

    void PrimerPaso()
    {
        Desde.Dar(this);
        segundoCompleto = false;
    }
    void SegundoPaso()
    {
        base.Pallets[0].transform.position = transform.position;
        segundoCompleto = true;
    }
    void TercerPaso()
    {
        Dar(Hasta);
        segundoCompleto = false;
    }

    public override void Dar(ManejoPallets receptor)
    {
        if (Tenencia())
        {
            if (receptor.Recibir(Pallets[0]))
            {
                Pallets.RemoveAt(0);
            }
        }
    }
    public override bool Recibir(Pallet pallet)
    {
        if (!Tenencia())
        {
            pallet.Portador = this.gameObject;
            base.Recibir(pallet);
            return true;
        }
        else
            return false;
    }
}
