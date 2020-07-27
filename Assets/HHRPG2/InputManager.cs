using SoftStar.Pal6;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InputManager
{
    private class KeyDefinition
    {
        private static float AxisRepeatTime = 0.5f;

        private static float AxisRepeatDelay = 1f;

        public KeyCode Code;

        public string Axis = string.Empty;

        public AxisDirectionType AxisDirection;

        private float m_lastAxisTime;

        private bool m_lastAxisState;

        private bool m_currentAxisState;

        public bool HasCode
        {
            get
            {
                return this.Code != KeyCode.None && this.Code != KeyCode.None;
            }
        }

        public bool HasAxis
        {
            get
            {
                return this.Axis != null && this.Axis != string.Empty;
            }
        }

        public bool AxisActive
        {
            get
            {
                return this.HasAxis && this.getAxisActive(true);
            }
        }

        public bool AxisActive_SinglePad
        {
            get
            {
                return this.HasAxis && this.getAxisActive(false);
            }
        }

        public bool AxisDown
        {
            get
            {
                return this.AxisActive && !this.m_lastAxisState;
            }
        }

        public bool AxisUp
        {
            get
            {
                return !this.AxisActive && this.m_lastAxisState;
            }
        }

        public bool AxisDown_SinglePad
        {
            get
            {
                return this.AxisActive_SinglePad && !this.m_lastAxisState;
            }
        }

        public bool AxisUp_SinglePad
        {
            get
            {
                return !this.AxisActive_SinglePad && this.m_lastAxisState;
            }
        }

        public float GetAxis
        {
            get
            {
                return this.getAxis(true);
            }
        }

        public float GetAxis_SinglePad
        {
            get
            {
                return this.getAxis(false);
            }
        }

        public float AxisRaw
        {
            get
            {
                return this.getAxisRaw(true);
            }
        }

        public float AxisRaw_SinglePad
        {
            get
            {
                return this.getAxisRaw(false);
            }
        }

        private float getAxisRaw(bool global = false)
        {
            float axisRaw;
            if (global)
            {
                axisRaw = Input.GetAxisRaw(this.Axis);
            }
            else
            {
                axisRaw = Input.GetAxisRaw(this.Axis);
            }
            return axisRaw;
        }

        private float getAxis(bool global = false)
        {
            float axis;
            if (global)
            {
                axis = Input.GetAxis(this.Axis);
            }
            else
            {
                axis = Input.GetAxis(this.Axis);
            }
            return axis;
        }

        private bool getAxisActive(bool global = false)
        {
            float axis = this.getAxis(global);
            if (this.m_lastAxisTime == 0f || this.m_lastAxisTime != Time.time)
            {
                this.m_lastAxisTime = Time.time;
                this.m_lastAxisState = this.m_currentAxisState;
            }
            if ((axis > InputManager.AxisKeyThreshold && this.AxisDirection == AxisDirectionType.Positive) || (axis < -InputManager.AxisKeyThreshold && this.AxisDirection == AxisDirectionType.Negative) || (Mathf.Abs(axis) > InputManager.AxisKeyThreshold && this.AxisDirection == AxisDirectionType.Both))
            {
                this.m_currentAxisState = true;
                return true;
            }
            this.m_currentAxisState = false;
            return false;
        }
    }

    private static bool s_lockThisFrame = false;

    private static float AxisActiveThreshold = 0.1f;

    private static float AxisKeyThreshold = 0.3f;

    public static bool bActive = true;

    private static Dictionary<KEY_ACTION, InputManager.KeyDefinition> GameKeyList = new Dictionary<KEY_ACTION, InputManager.KeyDefinition>();

    public static KeyDirection curKeyDir = KeyDirection.NONE;

    private static Vector3 m_MoveDir = Vector3.zero;

    private static string SaveFileName = "InputSettings";

    //	public static void LockThisFrame()
    //	{
    //		InputManager.s_lockThisFrame = true;
    //	}

    //	public static void SetTopUI(IUIInput ui)
    //	{
    //		InputManager.m_top = ui;
    //	}

    //	public static void PushUI(IUIInput ui)
    //	{
    //		InputManager.m_uiStack.Push(ui);
    //	}

    //	public static void PopUI(IUIInput ui)
    //	{
    //		if (InputManager.m_uiStack.Count <= 0 || InputManager.m_uiStack.Peek() != ui)
    //		{
    //			Debug.LogError(string.Format("Pop UI not match! witch current={0} and poping target={1}", InputManager.CurrentHandle.ToString(), ui.ToString()));
    //			return;
    //		}
    //		InputManager.m_uiStack.Pop();
    //	}

    //	public static void PushInteract(IUIInput interact)
    //	{
    //		InputManager.m_interactStack.Push(interact);
    //	}

    //	public static void PopInteract(IUIInput interact)
    //	{
    //		if (InputManager.m_interactStack.Count <= 0 || InputManager.m_interactStack.Peek() != interact)
    //		{
    //			Debug.LogError(string.Format("Pop Interact not match! witch current={0} and poping target={1}", InputManager.CurrentInteract.ToString(), interact.ToString()));
    //			return;
    //		}
    //		InputManager.m_interactStack.Pop();
    //	}

    public static void Initialize()
    {
        GameKeyList.Clear();
        InputManager.InitDefalutKeyMapping();
        PalMain.GameMain.updateHandles += new PalMain.void_func_float_float(InputManager.Update);
    }

    public static void InitDefalutKeyMapping()
    {
        InputManager.GameKeyList.Add(KEY_ACTION.UP, new InputManager.KeyDefinition
        {
            Code = KeyCode.W
        });
        InputManager.GameKeyList.Add(KEY_ACTION.DOWN, new InputManager.KeyDefinition
        {
            Code = KeyCode.S
        });
        InputManager.GameKeyList.Add(KEY_ACTION.LEFT, new InputManager.KeyDefinition
        {
            Code = KeyCode.A
        });
        InputManager.GameKeyList.Add(KEY_ACTION.RIGHT, new InputManager.KeyDefinition
        {
            Code = KeyCode.D
        });
        InputManager.GameKeyList.Add(KEY_ACTION.UPARROW, new InputManager.KeyDefinition
        {
            Code = KeyCode.UpArrow
        });
        InputManager.GameKeyList.Add(KEY_ACTION.DOWNARROW, new InputManager.KeyDefinition
        {
            Code = KeyCode.DownArrow
        });
        InputManager.GameKeyList.Add(KEY_ACTION.LEFTARROW, new InputManager.KeyDefinition
        {
            Code = KeyCode.LeftArrow
        });
        InputManager.GameKeyList.Add(KEY_ACTION.RIGHTARROW, new InputManager.KeyDefinition
        {
            Code = KeyCode.RightArrow
        });
        InputManager.GameKeyList.Add(KEY_ACTION.CAMERA_LEFT, new InputManager.KeyDefinition
        {
            Code = KeyCode.Q
        });
        InputManager.GameKeyList.Add(KEY_ACTION.CAMERA_RIGHT, new InputManager.KeyDefinition
        {
            Code = KeyCode.E
        });
        InputManager.GameKeyList.Add(KEY_ACTION.MOUSE_RIGHT, new InputManager.KeyDefinition
        {
            Code = KeyCode.Mouse1
        });
        InputManager.GameKeyList.Add(KEY_ACTION.MOUSE_LEFT, new InputManager.KeyDefinition
        {
            Code = KeyCode.Mouse0
        });
        InputManager.GameKeyList.Add(KEY_ACTION.JUMP, new InputManager.KeyDefinition
        {
            Code = KeyCode.Space
        });
        InputManager.GameKeyList.Add(KEY_ACTION.CONFIRM, new InputManager.KeyDefinition
        {
            Code = KeyCode.Space
        });
        InputManager.GameKeyList.Add(KEY_ACTION.CANCEL, new InputManager.KeyDefinition
        {
            Code = KeyCode.Escape
        });
        InputManager.GameKeyList.Add(KEY_ACTION.MENU, new InputManager.KeyDefinition
        {
            Code = KeyCode.Escape
        });
        InputManager.GameKeyList.Add(KEY_ACTION.TAB, new InputManager.KeyDefinition
        {
            Code = KeyCode.Tab
        });
        InputManager.GameKeyList.Add(KEY_ACTION.ACTION, new InputManager.KeyDefinition
        {
            Code = KeyCode.F
        });
        InputManager.GameKeyList.Add(KEY_ACTION.MAP, new InputManager.KeyDefinition
        {
            Code = KeyCode.M
        });
        InputManager.GameKeyList.Add(KEY_ACTION.BigMap, new InputManager.KeyDefinition
        {
            Code = KeyCode.T
        });
        InputManager.GameKeyList.Add(KEY_ACTION.WALK, new InputManager.KeyDefinition
        {
            Code = KeyCode.LeftShift
        });
        InputManager.GameKeyList.Add(KEY_ACTION.AUTO_WALK, new InputManager.KeyDefinition
        {
            Code = KeyCode.R
        });
        InputManager.GameKeyList.Add(KEY_ACTION.OTHER, new InputManager.KeyDefinition
        {
            Code = KeyCode.BackQuote
        });
        InputManager.GameKeyList.Add(KEY_ACTION.UI_SAVE, new InputManager.KeyDefinition
        {
            Code = KeyCode.C
        });
        InputManager.GameKeyList.Add(KEY_ACTION.UI_LOAD, new InputManager.KeyDefinition
        {
            Code = KeyCode.L
        });
        InputManager.GameKeyList.Add(KEY_ACTION.UI_QUEUE, new InputManager.KeyDefinition
        {
            Code = KeyCode.F1
        });
        InputManager.GameKeyList.Add(KEY_ACTION.UI_STATE, new InputManager.KeyDefinition
        {
            Code = KeyCode.F2
        });
        InputManager.GameKeyList.Add(KEY_ACTION.UI_EQUIP, new InputManager.KeyDefinition
        {
            Code = KeyCode.F3
        });
        InputManager.GameKeyList.Add(KEY_ACTION.UI_Symbol, new InputManager.KeyDefinition
        {
            Code = KeyCode.F4
        });
        InputManager.GameKeyList.Add(KEY_ACTION.UI_ITEM, new InputManager.KeyDefinition
        {
            Code = KeyCode.F5
        });
        InputManager.GameKeyList.Add(KEY_ACTION.UI_SKILL, new InputManager.KeyDefinition
        {
            Code = KeyCode.F6
        });
        InputManager.GameKeyList.Add(KEY_ACTION.UI_SOUL, new InputManager.KeyDefinition
        {
            Code = KeyCode.F7
        });
        InputManager.GameKeyList.Add(KEY_ACTION.UI_Compound, new InputManager.KeyDefinition
        {
            Code = KeyCode.F8
        });
        InputManager.GameKeyList.Add(KEY_ACTION.UI_Information, new InputManager.KeyDefinition
        {
            Code = KeyCode.F9
        });
        InputManager.GameKeyList.Add(KEY_ACTION.UI_System, new InputManager.KeyDefinition
        {
            Code = KeyCode.F10
        });
        InputManager.GameKeyList.Add(KEY_ACTION.BATTLE_FIRST, new InputManager.KeyDefinition
        {
            Code = KeyCode.F1
        });
        InputManager.GameKeyList.Add(KEY_ACTION.BATTLE_SECOND, new InputManager.KeyDefinition
        {
            Code = KeyCode.F2
        });
        InputManager.GameKeyList.Add(KEY_ACTION.BATTLE_THIRD, new InputManager.KeyDefinition
        {
            Code = KeyCode.F3
        });
        InputManager.GameKeyList.Add(KEY_ACTION.BATTLE_FOURTH, new InputManager.KeyDefinition
        {
            Code = KeyCode.F4
        });
        InputManager.GameKeyList.Add(KEY_ACTION.BATTLE_FIFTH, new InputManager.KeyDefinition
        {
            Code = KeyCode.F6
        });
        InputManager.GameKeyList.Add(KEY_ACTION.SKILL_FIRST, new InputManager.KeyDefinition
        {
            Code = KeyCode.Alpha1
        });
        InputManager.GameKeyList.Add(KEY_ACTION.SKILL_SECOND, new InputManager.KeyDefinition
        {
            Code = KeyCode.Alpha2
        });
        InputManager.GameKeyList.Add(KEY_ACTION.SKILL_THIRD, new InputManager.KeyDefinition
        {
            Code = KeyCode.Alpha3
        });
        InputManager.GameKeyList.Add(KEY_ACTION.SKILL_FOURTH, new InputManager.KeyDefinition
        {
            Code = KeyCode.Alpha4
        });
        InputManager.GameKeyList.Add(KEY_ACTION.SKILL_FIFTH, new InputManager.KeyDefinition
        {
            Code = KeyCode.Alpha5
        });
        InputManager.GameKeyList.Add(KEY_ACTION.SKILL_SIXTH, new InputManager.KeyDefinition
        {
            Code = KeyCode.Alpha6
        });
        InputManager.GameKeyList.Add(KEY_ACTION.SKILL_SEVENTH, new InputManager.KeyDefinition
        {
            Code = KeyCode.Alpha7
        });
        InputManager.GameKeyList.Add(KEY_ACTION.SKILL_EIGHTH, new InputManager.KeyDefinition
        {
            Code = KeyCode.Alpha8
        });
        InputManager.GameKeyList.Add(KEY_ACTION.Screenshot, new InputManager.KeyDefinition
        {
            Code = KeyCode.F12
        });
        InputManager.GameKeyList.Add(KEY_ACTION.CHAGNESTATE, new InputManager.KeyDefinition
        {
            Code = KeyCode.J
        });
        InputManager.GameKeyList.Add(KEY_ACTION.FIGHT1, new InputManager.KeyDefinition
        {
            Code = KeyCode.Z
        });
        InputManager.GameKeyList.Add(KEY_ACTION.TAB_SNEAK, new InputManager.KeyDefinition
        {
            Code = KeyCode.LeftControl
        });
    }

    public static bool GetKeyDown(KEY_ACTION key, bool isGlobal = false)
    {
        if (!InputManager.bActive || InputManager.s_lockThisFrame)
        {
            return false;
        }
        if (InputManager.GameKeyList[key].HasCode)
        {
            bool keyDown;
            if (isGlobal)
            {
                keyDown = Input.GetKeyDown(InputManager.GameKeyList[key].Code);
            }
            else
            {
                keyDown = Input.GetKeyDown(InputManager.GameKeyList[key].Code);
            }
            return keyDown;
        }
        if (InputManager.GameKeyList[key].HasAxis)
        {
            bool axisDown;
            if (isGlobal)
            {
                axisDown = InputManager.GameKeyList[key].AxisDown;
            }
            else
            {
                axisDown = InputManager.GameKeyList[key].AxisDown;
            }
            return axisDown;
        }
        return false;
    }

    public static bool GetKeyUp(KEY_ACTION key, bool isGlobal = false)
    {
        if (!InputManager.bActive || InputManager.s_lockThisFrame)
        {
            return false;
        }
        if (InputManager.GameKeyList[key].HasCode)
        {
            bool keyUp;
            if (isGlobal)
            {
                keyUp = Input.GetKeyUp(InputManager.GameKeyList[key].Code);
            }
            else
            {
                keyUp = Input.GetKeyUp(InputManager.GameKeyList[key].Code);
            }
            return keyUp;
        }
        if (InputManager.GameKeyList[key].HasAxis)
        {
            bool axisUp;
            if (isGlobal)
            {
                axisUp = InputManager.GameKeyList[key].AxisUp;
            }
            else
            {
                axisUp = InputManager.GameKeyList[key].AxisUp;
            }
            return axisUp;
        }
        return false;
    }

    public static bool GetKey(KEY_ACTION key, bool isGlobal = false)
    {
        if (!InputManager.bActive || InputManager.s_lockThisFrame)
        {
            return false;
        }
        if (InputManager.GameKeyList[key].HasCode)
        {
            bool key2;
            if (isGlobal)
            {
                key2 = Input.GetKey(InputManager.GameKeyList[key].Code);
            }
            else
            {
                key2 = Input.GetKey(InputManager.GameKeyList[key].Code);
            }
            return key2;
        }
        if (InputManager.GameKeyList[key].HasAxis)
        {
            bool axisActive;
            if (isGlobal)
            {
                axisActive = InputManager.GameKeyList[key].AxisActive;
            }
            else
            {
                axisActive = InputManager.GameKeyList[key].AxisActive;
            }
            return axisActive;
        }
        return false;
    }

    //	public static float GetAxis(KEY_ACTION key, bool isGlobal = false)
    //	{
    //		if (!InputManager.bActive || InputManager.s_lockThisFrame)
    //		{
    //			return 0f;
    //		}
    //		if (!InputManager.GameKeyList[key].HasAxis)
    //		{
    //			return 0f;
    //		}
    //		float axis;
    //		if (isGlobal)
    //		{
    //			axis = Input.GetAxis(InputManager.GameKeyList[key].Axis);
    //		}
    //		else
    //		{
    //			axis = Input.GetAxis(InputManager.GameKeyList[key].Axis);
    //		}
    //		if (InputManager.GameKeyList[key].AxisDirection == AxisDirectionType.Both)
    //		{
    //			return axis;
    //		}
    //		if ((axis > 0f && InputManager.GameKeyList[key].AxisDirection == AxisDirectionType.Positive) || (axis < 0f && InputManager.GameKeyList[key].AxisDirection == AxisDirectionType.Negative))
    //		{
    //			return Mathf.Abs(axis);
    //		}
    //		return 0f;
    //	}

    //	public static float GetAxisRaw(KEY_ACTION key, bool isGlobal = false)
    //	{
    //		if (!InputManager.bActive || InputManager.s_lockThisFrame)
    //		{
    //			return 0f;
    //		}
    //		if (!InputManager.GameKeyList[key].HasAxis)
    //		{
    //			return 0f;
    //		}
    //		float axisRaw;
    //		if (isGlobal)
    //		{
    //			axisRaw = Input.GetAxisRaw(InputManager.GameKeyList[key].Axis);
    //		}
    //		else
    //		{
    //			axisRaw = Input.GetAxisRaw(InputManager.GameKeyList[key].Axis);
    //		}
    //		if (InputManager.GameKeyList[key].AxisDirection == AxisDirectionType.Both)
    //		{
    //			return axisRaw;
    //		}
    //		if ((axisRaw > 0f && InputManager.GameKeyList[key].AxisDirection == AxisDirectionType.Positive) || (axisRaw < 0f && InputManager.GameKeyList[key].AxisDirection == AxisDirectionType.Negative))
    //		{
    //			return Mathf.Abs(axisRaw);
    //		}
    //		return 0f;
    //	}

    //	public static bool GetAxisActive(KEY_ACTION key, bool isGlobal = false)
    //	{
    //		if (!InputManager.bActive || InputManager.s_lockThisFrame)
    //		{
    //			return false;
    //		}
    //		if (InputManager.GameKeyList[key].HasAxis)
    //		{
    //			bool result;
    //			if (isGlobal)
    //			{
    //				result = InputManager.GameKeyList[key].AxisActive;
    //			}
    //			else
    //			{
    //				result = InputManager.GameKeyList[key].AxisActive_SinglePad;
    //			}
    //			return result;
    //		}
    //		return false;
    //	}

    //	public static void SetMappingKey(KEY_ACTION keyIndex, KeyCode keyCode)
    //	{
    //		InputManager.GameKeyList[keyIndex].Code = keyCode;
    //	}

    //	public static void SetMappingAxis(KEY_ACTION keyIndex, string axis, AxisDirectionType dir)
    //	{
    //		InputManager.GameKeyList[keyIndex].Axis = axis;
    //		InputManager.GameKeyList[keyIndex].AxisDirection = dir;
    //	}

    //	public static KeyCode GetMappingKey(KEY_ACTION keyIndex)
    //	{
    //		if (!InputManager.GameKeyList.ContainsKey(keyIndex))
    //		{
    //			return KeyCode.None;
    //		}
    //		return InputManager.GameKeyList[keyIndex].Code;
    //	}

    //	public static string GetMappingAxis(KEY_ACTION keyIndex)
    //	{
    //		if (!InputManager.GameKeyList.ContainsKey(keyIndex))
    //		{
    //			return string.Empty;
    //		}
    //		return InputManager.GameKeyList[keyIndex].Axis;
    //	}

    //	public static AxisDirectionType GetMappingAxisDirection(KEY_ACTION keyIndex)
    //	{
    //		if (!InputManager.GameKeyList.ContainsKey(keyIndex))
    //		{
    //			return AxisDirectionType.None;
    //		}
    //		return InputManager.GameKeyList[keyIndex].AxisDirection;
    //	}

    private static void UpdateDir()
    {
        float num = 0f;
        float num2 = 0f;
        float num3 = 0f;
        float num4 = 0f;
        if (InputManager.GetKey(KEY_ACTION.UPARROW, false) || InputManager.GetKey(KEY_ACTION.UP, false))
        {
            num = 1f;
        }
        else if (InputManager.GetKey(KEY_ACTION.DOWNARROW, false) || InputManager.GetKey(KEY_ACTION.DOWN, false))
        {
            num2 = 1f;
        }
        if (InputManager.GetKey(KEY_ACTION.LEFTARROW, false) || InputManager.GetKey(KEY_ACTION.LEFT, false))
        {
            num3 = 1f;
        }
        else if (InputManager.GetKey(KEY_ACTION.RIGHTARROW, false) || InputManager.GetKey(KEY_ACTION.RIGHT, false))
        {
            num4 = 1f;
        }
        if (num > 0f)
        {
            InputManager.m_MoveDir.z = num;
            InputManager.curKeyDir |= KeyDirection.W;
            if ((InputManager.curKeyDir & KeyDirection.S) > KeyDirection.NONE)
            {
                InputManager.curKeyDir ^= KeyDirection.S;
            }
        }
        else if (num2 > 0f)
        {
            InputManager.m_MoveDir.z = num2 * -1f;
            InputManager.curKeyDir |= KeyDirection.S;
            if ((InputManager.curKeyDir & KeyDirection.W) > KeyDirection.NONE)
            {
                InputManager.curKeyDir ^= KeyDirection.W;
            }
        }
        else
        {
            InputManager.m_MoveDir.z = 0f;
            if ((InputManager.curKeyDir & KeyDirection.S) > KeyDirection.NONE)
            {
                InputManager.curKeyDir ^= KeyDirection.S;
            }
            if ((InputManager.curKeyDir & KeyDirection.W) > KeyDirection.NONE)
            {
                InputManager.curKeyDir ^= KeyDirection.W;
            }
        }
        if (num3 > 0f)
        {
            InputManager.m_MoveDir.x = num3 * -1f;
            InputManager.curKeyDir |= KeyDirection.A;
            if ((InputManager.curKeyDir & KeyDirection.D) > KeyDirection.NONE)
            {
                InputManager.curKeyDir ^= KeyDirection.D;
            }
        }
        else if (num4 > 0f)
        {
            InputManager.m_MoveDir.x = num4;
            InputManager.curKeyDir |= KeyDirection.D;
            if ((InputManager.curKeyDir & KeyDirection.A) > KeyDirection.NONE)
            {
                InputManager.curKeyDir ^= KeyDirection.A;
            }
        }
        else
        {
            InputManager.m_MoveDir.x = 0f;
            if ((InputManager.curKeyDir & KeyDirection.D) > KeyDirection.NONE)
            {
                InputManager.curKeyDir ^= KeyDirection.D;
            }
            if ((InputManager.curKeyDir & KeyDirection.A) > KeyDirection.NONE)
            {
                InputManager.curKeyDir ^= KeyDirection.A;
            }
        }
    }

    private static void Update(float curTime, float deltaTime)
    {
        InputManager.s_lockThisFrame = false;
    }

    public static Vector3 GetDir()
    {
        if (!InputManager.bActive)
        {
            return Vector3.zero;
        }
        InputManager.UpdateDir();
        return InputManager.m_MoveDir;
    }

    //	public static void Save()
    //	{
    //		string storeDirePath = InputManager.GetStoreDirePath();
    //		using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(storeDirePath, FileMode.Create)))
    //		{
    //			binaryWriter.Write(InputManager.GameKeyList.Count);
    //			foreach (KeyValuePair<KEY_ACTION, InputManager.KeyDefinition> current in InputManager.GameKeyList)
    //			{
    //				binaryWriter.Write((int)current.Key);
    //				binaryWriter.Write((int)current.Value.Code);
    //				binaryWriter.Write(current.Value.Axis);
    //				binaryWriter.Write((byte)current.Value.AxisDirection);
    //			}
    //		}
    //	}
}
