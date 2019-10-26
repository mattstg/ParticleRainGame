using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager 
{
    #region Singleton
    public static InputManager Instance
    {
        get
        {
            return instance ?? (instance = new InputManager());
        }
    }

    private static InputManager instance;

    private InputManager() { }
    #endregion
    public InputPkg fixedUpdateInputPkg;
    public InputPkg updateInputPkg;
    public void Initialize()
    {
        fixedUpdateInputPkg = new InputPkg();
        updateInputPkg = new InputPkg();
    }

    public void PhysicsRefresh()
    {
        FillInputPkg(fixedUpdateInputPkg);
    }

    public void PostInitialize()
    {

    }

    private void FillInputPkg(InputPkg toFill)
    {
        if (!toFill.jumpFirstPressed && Input.GetAxis("Horizontal") > 0)
            toFill.jumpFirstPressed = true;
        else
            toFill.jumpFirstPressed = false;

        Vector2 dirPressed = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (dirPressed != Vector2.zero)
            toFill.lastNonZeroInput = dirPressed;
        toFill.moveInput = dirPressed;
        toFill.useHosePressed = Input.GetButton("Fire");
    }

    public void Refresh()
    {
        FillInputPkg(updateInputPkg);
    }

    public class InputPkg
    {
        public Vector2 lastNonZeroInput;
        public Vector2 moveInput;
        public bool jumpFirstPressed;
        public bool useHosePressed;

    }
}
