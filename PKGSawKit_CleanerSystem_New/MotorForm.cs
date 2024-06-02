using Ajin_motion_driver;
using System;
using System.Reflection;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace PKGSawKit_CleanerSystem_New
{
    public partial class MotorForm : UserControl
    {
        private MaintnanceForm m_Parent;

        AnalogDlg analogDlg;
        DigitalDlg digitalDlg;

        int module;
        string ModuleName;
        bool bDisplay;

        private Timer logdisplayTimer = new Timer();

        private TextBox[] m_servoBox;
        private TextBox[] m_runStsBox;
        private TextBox[] m_almStsBox;
        private TextBox[] m_limitStsBox;
        private TextBox[] m_actSpdBox;
        private TextBox[] m_setSpdBox;
        private TextBox[] m_actAccelBox;
        private TextBox[] m_setAccelBox;
        private TextBox[] m_actDecelBox;
        private TextBox[] m_setDecelBox;
        private TextBox[] m_actGearBox;
        private TextBox[] m_setGearBox;
        private TextBox[] m_actPositionBox;               
        private TextBox[] m_setPositionBox;

        public MotorForm(MaintnanceForm parent)
        {
            m_Parent = parent;
            
            module = (int)MODULE._MOTOR;
            ModuleName = "MOTOR";

            InitializeComponent();               
        }

        private void MotorForm_Load(object sender, EventArgs e)
        {
            Width = 1172;
            Height = 824;
            Top = 0;
            Left = 0;

            m_servoBox = new TextBox[(int)MotionDefine.Axis_max - 1] { textBoxAxis0Servo, textBoxAxis1Servo, textBoxAxis2Servo };
            m_runStsBox = new TextBox[(int)MotionDefine.Axis_max - 1] { textBoxAxis0Runsts, textBoxAxis1Runsts, textBoxAxis2Runsts };            
            m_limitStsBox = new TextBox[(int)MotionDefine.Axis_max - 1] { textBoxAxis0Limit, textBoxAxis1Limit, textBoxAxis2Limit };
            m_actSpdBox = new TextBox[(int)MotionDefine.Axis_max - 1] { textBoxAxis0SpeedCur, textBoxAxis1SpeedCur, textBoxAxis2SpeedCur };
            m_setSpdBox = new TextBox[(int)MotionDefine.Axis_max - 1] { textBoxAxis0SpeedSet, textBoxAxis1SpeedSet, textBoxAxis2SpeedSet };
            m_actAccelBox = new TextBox[(int)MotionDefine.Axis_max - 1] { textBoxAxis0AccelCur, textBoxAxis1AccelCur, textBoxAxis2AccelCur };
            m_setAccelBox = new TextBox[(int)MotionDefine.Axis_max - 1] { textBoxAxis0AccelSet, textBoxAxis1AccelSet, textBoxAxis2AccelSet };
            m_actDecelBox = new TextBox[(int)MotionDefine.Axis_max - 1] { textBoxAxis0DecelCur, textBoxAxis1DecelCur, textBoxAxis2DecelCur };
            m_setDecelBox = new TextBox[(int)MotionDefine.Axis_max - 1] { textBoxAxis0DecelSet, textBoxAxis1DecelSet, textBoxAxis2DecelSet };
            m_actGearBox = new TextBox[(int)MotionDefine.Axis_max - 1] { textBoxAxis0GearCur, textBoxAxis1GearCur, textBoxAxis2GearCur };
            m_setGearBox = new TextBox[(int)MotionDefine.Axis_max - 1] { textBoxAxis0GearSet, textBoxAxis1GearSet, textBoxAxis2GearSet };
            m_actPositionBox = new TextBox[(int)MotionDefine.Axis_max - 1] { textBoxAxis0PositionCur, textBoxAxis1PositionCur, textBoxAxis2PositionCur };
            m_setPositionBox = new TextBox[(int)MotionDefine.Axis_max - 1] { textBoxAxis0PositionSet, textBoxAxis1PositionSet, textBoxAxis2PositionSet };

            bDisplay = true;

            Value_Init();

            //logdisplayTimer.Interval = 100;
            //logdisplayTimer.Elapsed += new ElapsedEventHandler(Eventlog_Display);
            //logdisplayTimer.Start();
        }

        private void SetDoubleBuffered(Control control, bool doubleBuffered = true)
        {
            PropertyInfo propertyInfo = typeof(Control).GetProperty
            (
                "DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic
            );
            propertyInfo.SetValue(control, doubleBuffered, null);
        }

        private void Value_Init()
        {
            double dBrushUpDownAcelDecl;
            double dBrushRotationAcelDecl;
            double dWaterBlockMoveAcelDecl;
            
            dBrushUpDownAcelDecl = Configure_List.Brush_UpDown_Speed * 2;
            dBrushRotationAcelDecl = Configure_List.Brush_Rotation_Speed * 2;
            dWaterBlockMoveAcelDecl = Configure_List.WaterBlock_Move_Speed * 2;

            MotionClass.SetMotorVelocity(Define.axis_z, Configure_List.Brush_UpDown_Speed);
            MotionClass.SetMotorVelocity(Define.axis_r, Configure_List.Brush_Rotation_Speed);
            MotionClass.SetMotorVelocity(Define.axis_y, Configure_List.WaterBlock_Move_Speed);

            MotionClass.SetMotorAccel(Define.axis_z, dBrushUpDownAcelDecl);
            MotionClass.SetMotorAccel(Define.axis_r, dBrushRotationAcelDecl);
            MotionClass.SetMotorAccel(Define.axis_y, dWaterBlockMoveAcelDecl);

            MotionClass.SetMotorDecel(Define.axis_z, dBrushUpDownAcelDecl);
            MotionClass.SetMotorDecel(Define.axis_r, dBrushRotationAcelDecl);
            MotionClass.SetMotorDecel(Define.axis_y, dWaterBlockMoveAcelDecl);

            MotionClass.SetMotorGearing(Define.axis_z, 1);
            MotionClass.SetMotorGearing(Define.axis_r, 1);
            MotionClass.SetMotorGearing(Define.axis_y, 1);            
        }

        public void Display()
        {
            SetDoubleBuffered(groupBoxAxis0);
            SetDoubleBuffered(groupBoxAxis1);
            SetDoubleBuffered(groupBoxAxis2);
            
            if (bDisplay)
            {
                for (int nAxis = 0; nAxis < MotionDefine.Axis_max - 1; nAxis++)
                {
                    m_servoBox[nAxis].Text = MotionClass.motor[nAxis].sR_ServoStatus;
                    m_runStsBox[nAxis].Text = MotionClass.motor[nAxis].sR_BusyStatus;                    
                    m_limitStsBox[nAxis].Text = MotionClass.motor[nAxis].sR_HomeStatus;
                    m_actSpdBox[nAxis].Text = string.Format("{0:0.0}", MotionClass.motor[nAxis].dR_CmdVelocity);
                    m_actAccelBox[nAxis].Text = string.Format("{0:0.0}", MotionClass.motor[nAxis].dW_Accel);
                    m_actDecelBox[nAxis].Text = string.Format("{0:0.0}", MotionClass.motor[nAxis].dW_Decel);
                    m_actGearBox[nAxis].Text = string.Format("{0:0.0}", MotionClass.motor[nAxis].dW_Gearing);
                    m_actPositionBox[nAxis].Text = string.Format("{0:0.000}", MotionClass.motor[nAxis].dR_ActPosition_step);

                    m_setSpdBox[nAxis].Text = string.Format("{0:0.0}", MotionClass.motor[nAxis].dW_Velocity);
                    m_setAccelBox[nAxis].Text = string.Format("{0:0.0}", MotionClass.motor[nAxis].dW_Accel);
                    m_setDecelBox[nAxis].Text = string.Format("{0:0.0}", MotionClass.motor[nAxis].dW_Decel);
                    m_setGearBox[nAxis].Text = string.Format("{0:0.0}", MotionClass.motor[nAxis].dW_Gearing);
                    m_setPositionBox[nAxis].Text = string.Format("{0:0.000}", MotionClass.motor[nAxis].dW_Position_mm);
                }


                if (MotionClass.motor[Define.axis_z].sR_ServoStatus == "SVOFF")
                {
                    if (btnAxis0SVOFF.Enabled != false)
                        btnAxis0SVOFF.Enabled = false;

                    if (!btnAxis0SVON.Enabled)
                        btnAxis0SVON.Enabled = true;
                }
                else
                {
                    if (!btnAxis0SVOFF.Enabled)
                        btnAxis0SVOFF.Enabled = true;

                    if (btnAxis0SVON.Enabled != false)
                        btnAxis0SVON.Enabled = false;
                }

                if (MotionClass.motor[Define.axis_r].sR_ServoStatus == "SVOFF")
                {
                    if (btnAxis1SVOFF.Enabled != false)
                        btnAxis1SVOFF.Enabled = false;

                    if (!btnAxis1SVON.Enabled)
                        btnAxis1SVON.Enabled = true;
                }
                else
                {
                    if (!btnAxis1SVOFF.Enabled)
                        btnAxis1SVOFF.Enabled = true;

                    if (btnAxis1SVON.Enabled != false)
                        btnAxis1SVON.Enabled = false;
                }

                if (MotionClass.motor[Define.axis_y].sR_ServoStatus == "SVOFF")
                {
                    if (btnAxis2SVOFF.Enabled != false)
                        btnAxis2SVOFF.Enabled = false;

                    if (!btnAxis2SVON.Enabled)
                        btnAxis2SVON.Enabled = true;
                }
                else
                {
                    if (!btnAxis2SVOFF.Enabled)
                        btnAxis2SVOFF.Enabled = true;

                    if (btnAxis2SVON.Enabled != false)
                        btnAxis2SVON.Enabled = false;
                }


                if (MotionClass.motor[Define.axis_z].sR_BusyStatus == "Moving")
                {
                    if (btnAxis0AlarmReset.Enabled != false)
                        btnAxis0AlarmReset.Enabled = false;

                    if (btnAxis0Home.Enabled != false)
                        btnAxis0Home.Enabled = false;

                    if (btnAxis0ZeroSet.Enabled != false)
                        btnAxis0ZeroSet.Enabled = false;
                }
                else
                {
                    if (!btnAxis0AlarmReset.Enabled)
                        btnAxis0AlarmReset.Enabled = true;

                    if (!btnAxis0Home.Enabled)
                        btnAxis0Home.Enabled = true;

                    if (!btnAxis0ZeroSet.Enabled)
                        btnAxis0ZeroSet.Enabled = true;
                }

                if (MotionClass.motor[Define.axis_r].sR_BusyStatus == "Moving")
                {
                    if (btnAxis1AlarmReset.Enabled != false)
                        btnAxis1AlarmReset.Enabled = false;

                    if (btnAxis1Home.Enabled != false)
                        btnAxis1Home.Enabled = false;

                    if (btnAxis1ZeroSet.Enabled != false)
                        btnAxis1ZeroSet.Enabled = false;
                }
                else
                {
                    if (!btnAxis1AlarmReset.Enabled)
                        btnAxis1AlarmReset.Enabled = true;

                    if (!btnAxis1Home.Enabled)
                        btnAxis1Home.Enabled = true;

                    if (!btnAxis1ZeroSet.Enabled)
                        btnAxis1ZeroSet.Enabled = true;
                }

                if (MotionClass.motor[Define.axis_y].sR_BusyStatus == "Moving")
                {
                    if (btnAxis2AlarmReset.Enabled != false)
                        btnAxis2AlarmReset.Enabled = false;

                    if (btnAxis2Home.Enabled != false)
                        btnAxis2Home.Enabled = false;

                    if (btnAxis2ZeroSet.Enabled != false)
                        btnAxis2ZeroSet.Enabled = false;
                }
                else
                {
                    if (!btnAxis2AlarmReset.Enabled)
                        btnAxis2AlarmReset.Enabled = true;

                    if (!btnAxis2Home.Enabled)
                        btnAxis2Home.Enabled = true;

                    if (!btnAxis2ZeroSet.Enabled)
                        btnAxis2ZeroSet.Enabled = true;
                }

                textBoxAxis2Alarm.Text = MotionClass.motor[Define.axis_y].sR_AlarmStatus;
            }                                                           
        }

        private void Velocity_Click(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;

            analogDlg = new AnalogDlg();
            analogDlg.Init();
            if (analogDlg.ShowDialog() == DialogResult.OK)
            {
                txtBox.Text = analogDlg.m_strResult.ToString();
                double dVal = Convert.ToDouble(txtBox.Text);

                MotionClass.SetMotorVelocity(Convert.ToInt32(txtBox.Tag), dVal);
            }
        }

        private void Accel_Click(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;

            analogDlg = new AnalogDlg();
            analogDlg.Init();
            if (analogDlg.ShowDialog() == DialogResult.OK)
            {
                txtBox.Text = analogDlg.m_strResult.ToString();
                double dVal = Convert.ToDouble(txtBox.Text);

                MotionClass.SetMotorAccel(Convert.ToInt32(txtBox.Tag), dVal);
            }
        }

        private void Decel_Click(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;

            analogDlg = new AnalogDlg();
            analogDlg.Init();
            if (analogDlg.ShowDialog() == DialogResult.OK)
            {
                txtBox.Text = analogDlg.m_strResult.ToString();
                double dVal = Convert.ToDouble(txtBox.Text);

                MotionClass.SetMotorDecel(Convert.ToInt32(txtBox.Tag), dVal);
            }
        }

        private void Gearing_Click(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;

            analogDlg = new AnalogDlg();
            analogDlg.Init();
            if (analogDlg.ShowDialog() == DialogResult.OK)
            {
                txtBox.Text = analogDlg.m_strResult.ToString();
                double dVal = Convert.ToDouble(txtBox.Text);

                MotionClass.SetMotorGearing(Convert.ToInt32(txtBox.Tag), dVal);
            }
        }

        private void Move_Click(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;

            analogDlg = new AnalogDlg();
            analogDlg.Init();
            if (analogDlg.ShowDialog() == DialogResult.OK)
            {
                txtBox.Text = analogDlg.m_strResult.ToString();
                double dVal = Convert.ToDouble(txtBox.Text);

                if (txtBox.Tag.ToString() == "2")
                {
                    if (Global.MOTION_INTERLOCK_CHECK())
                    {
                        MotionClass.MotorMove(Convert.ToInt32(txtBox.Tag), dVal);
                    }
                    else
                    {
                        MessageBox.Show("EMO switch is on / Door is open", "Interlock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MotionClass.MotorMove(Convert.ToInt32(txtBox.Tag), dVal);
                }
            }
        }

        private void Digital_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;            
            string strTmp = btn.Text.ToString();

            switch (strTmp)
            {
                case "SVOFF":
                    {
                        MotionClass.SetMotorServo(Convert.ToInt32(btn.Tag), (uint)DigitalOffOn.Off);
                    }
                    break;

                case "SVON":
                    {
                        MotionClass.SetMotorServo(Convert.ToInt32(btn.Tag), (uint)DigitalOffOn.On);
                    }
                    break;

                case "Stop":
                    {
                        MotionClass.SetMotorSStop(Convert.ToInt32(btn.Tag));
                    }
                    break;

                case "Alarm reset":
                    {
                        MotionClass.SetAlarmReset(Convert.ToInt32(btn.Tag));
                    }
                    break;

                case "Home":
                    {
                        if (btn.Tag.ToString() == "2")
                        {
                            if (Global.MOTION_INTERLOCK_CHECK())
                            {
                                MotionClass.SetMotorHome(Convert.ToInt32(btn.Tag));
                            }
                            else
                            {
                                MessageBox.Show("EMO switch is on / Door is open", "Interlock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MotionClass.SetMotorHome(Convert.ToInt32(btn.Tag));
                        }                    
                    }
                    break;

                case "Zeroset":
                    {
                        MotionClass.SetZeroset(Convert.ToInt32(btn.Tag));
                    }
                    break;
            }
        }

        private void btnAxis0JogN_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;

            double dVelocity = MotionClass.motor[Convert.ToInt32(btn.Tag)].dW_Velocity;
            double dAccel = MotionClass.motor[Convert.ToInt32(btn.Tag)].dW_Accel;
            double dDecel = MotionClass.motor[Convert.ToInt32(btn.Tag)].dW_Decel;

            if (btn.Tag.ToString() == "2")
            {
                if (Global.MOTION_INTERLOCK_CHECK())
                {
                    MotionClass.MotorJogN(Convert.ToInt32(btn.Tag), dVelocity, dAccel, dDecel);
                }
                else
                {
                    MessageBox.Show("EMO switch is on / Door is open", "Interlock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MotionClass.MotorJogN(Convert.ToInt32(btn.Tag), dVelocity, dAccel, dDecel);
            }                     
        }

        private void btnAxis0JogN_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;

            MotionClass.SetMotorSStop(Convert.ToInt32(btn.Tag));
        }

        private void btnAxis0JogP_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;

            double dVelocity = MotionClass.motor[Convert.ToInt32(btn.Tag)].dW_Velocity;
            double dAccel = MotionClass.motor[Convert.ToInt32(btn.Tag)].dW_Accel;
            double dDecel = MotionClass.motor[Convert.ToInt32(btn.Tag)].dW_Decel;

            if (btn.Tag.ToString() == "2")
            {
                if (Global.MOTION_INTERLOCK_CHECK())
                {
                    MotionClass.MotorJogP(Convert.ToInt32(btn.Tag), dVelocity, dAccel, dDecel);
                }
                else
                {
                    MessageBox.Show("EMO switch is on / Door is open", "Interlock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MotionClass.MotorJogP(Convert.ToInt32(btn.Tag), dVelocity, dAccel, dDecel);
            }            
        }
    }
}
