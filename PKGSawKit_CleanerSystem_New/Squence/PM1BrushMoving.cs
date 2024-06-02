using Ajin_motion_driver;
using MsSqlManagerLibrary;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PKGSawKit_CleanerSystem_New.Squence
{
    class PM1BrushMoving : TBaseThread
    {
        Thread thread;
        private new TStep step;
        Alarm_List alarm_List;  // Alarm list

        public PM1BrushMoving()
        {
            ModuleName = "PM1";
            module = (byte)MODULE._PM1;
            
            thread = new Thread(new ThreadStart(Execute));
            
            alarm_List = new Alarm_List();            

            thread.Start();
        }

        public void Dispose()
        {
            thread.Abort();
        }

        private void Execute()
        {            
            try
            {
                while (true)
                {                    
                    if (Define.seqBrushFwBwCtrl == Define.CTRL_ABORT)
                    {
                        AlarmAction("Abort");
                    }
                    else if (Define.seqBrushFwBwCtrl == Define.CTRL_RETRY)
                    {
                        AlarmAction("Retry");
                    }

                    Run_Progress();
                    Home_Progress();
                    Clean_Progress();

                    Thread.Sleep(10);
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void AlarmAction(string sAction)
        {
            if (sAction == "Retry")
            {
                step.Flag = true;
                step.Times = 1;

                Define.seqBrushFwBwCtrl = Define.CTRL_RUNNING;

                if (Define.seqCtrl[module] == Define.CTRL_ALARM)
                {
                    Define.seqCtrl[module] = Define.CTRL_RUNNING;
                }
            }
            else if (sAction == "Abort")
            {
                ActionList();                               

                Define.seqBrushFwBwMode = Define.MODE_BRUSH_FWBW_IDLE;
                Define.seqBrushFwBwCtrl = Define.CTRL_IDLE;
                Define.seqBrushFwBwSts = Define.STS_BRUSH_FWBW_ABORTOK;

                step.Times = 1;                

                Global.EventLog("Brush cylinder movement stopped : " + sAction, ModuleName, "Event");
            }
        }

        private void ActionList()
        {
            F_PROCESS_ALL_VALVE_CLOSE();

            Global.SetDigValue((int)DigOutputList.CH1_Brush_Fwd_o, (uint)DigitalOffOn.Off, ModuleName);
            Global.SetDigValue((int)DigOutputList.CH1_Brush_Bwd_o, (uint)DigitalOffOn.Off, ModuleName);

            MotionClass.SetMotorSStop(Define.axis_r);            
        }

        private void ShowAlarm(string almId)
        {
            ActionList();

            Define.seqBrushFwBwCtrl = Define.CTRL_ALARM;

            // 프로세스 시퀀스 알람으로 멈춤
            Define.seqCtrl[module] = Define.CTRL_ALARM;

            // Buzzer IO On.
            Global.SetDigValue((int)DigOutputList.Buzzer_o, (uint)DigitalOffOn.On, ModuleName);

            // Alarm history.
            Define.sAlarmName = "";
            alarm_List.alarm_code = almId;
            Define.sAlarmName = alarm_List.alarm_code;

            Global.EventLog(almId + ":" + Define.sAlarmName, ModuleName, "Alarm");

            HostConnection.Host_Set_RunStatus(Global.hostEquipmentInfo, ModuleName, "Alarm");
            HostConnection.Host_Set_AlarmName(Global.hostEquipmentInfo, ModuleName, Define.sAlarmName);
        }

        public void F_INC_STEP()
        {
            step.Flag = true;
            step.Layer++;
            step.Times = 1;
        }

        // BRUSH CYLINDER PROGRESS //////////////////////////////////////////////////////////
        #region BRUSH CYLINDER_PROGRESS
        private void Run_Progress()
        {
            if ((Define.seqBrushFwBwMode == Define.MODE_BRUSH_FWBW_RUN) && (Define.seqBrushFwBwCtrl == Define.CTRL_RUN))
            {
                step.Layer = 1;
                step.Times = 1;
                step.Flag = true;

                Define.seqBrushFwBwCtrl = Define.CTRL_RUNNING;
                Define.seqBrushFwBwSts = Define.STS_BRUSH_FWBW_RUNING;                

                Global.EventLog("START THE BRUSH CYLINDER MOVING.", ModuleName, "Event");
            }
            else if ((Define.seqBrushFwBwMode == Define.MODE_BRUSH_FWBW_RUN) && (Define.seqBrushFwBwCtrl == Define.CTRL_RUNNING))
            {
                switch (step.Layer)
                {
                    case 1:
                        {
                            P_BRUSH_CYLINDER_FwdBwd("Forward");
                        }
                        break;

                    case 2:
                        {
                            P_BRUSH_CYLINDER_FwdBwd("Backward");
                        }
                        break;

                    case 3:
                        {
                            P_BRUSH_CYLINDER_StepCheck(1);
                        }
                        break;                    
                }
            }
        }

        private void Home_Progress()
        {
            if ((Define.seqBrushFwBwMode == Define.MODE_BRUSH_FWBW_HOME) && (Define.seqBrushFwBwCtrl == Define.CTRL_RUN))
            {
                Thread.Sleep(500);
                step.Layer = 1;
                step.Times = 1;
                step.Flag = true;

                Define.seqBrushFwBwCtrl = Define.CTRL_RUNNING;
                Define.seqBrushFwBwSts = Define.STS_BRUSH_FWBW_HOMEING;                

                Global.EventLog("START THE BRUSH CYLINDER HOME.", ModuleName, "Event");
            }
            else if ((Define.seqBrushFwBwMode == Define.MODE_BRUSH_FWBW_HOME) && (Define.seqBrushFwBwCtrl == Define.CTRL_RUNNING))
            {
                switch (step.Layer)
                {
                    case 1:
                        {
                            P_BRUSH_CYLINDER_FwdBwd_Home();
                        }
                        break;

                    case 2:
                        {
                            P_BRUSH_CYLINDER_FwdBwd_HomeEnd();
                        }
                        break;                    
                }
            }
        }

        private void Clean_Progress()
        {
            if ((Define.seqBrushFwBwMode == Define.MODE_BRUSH_FWBW_CLEAN) && (Define.seqBrushFwBwCtrl == Define.CTRL_RUN))
            {
                Thread.Sleep(500);
                step.Layer = 1;
                step.Times = 1;
                step.Flag = true;

                Define.seqBrushFwBwCtrl = Define.CTRL_RUNNING;
                Define.seqBrushFwBwSts = Define.STS_BRUSH_FWBW_CLEANING;                

                Global.EventLog("START THE BRUSH CYLINDER CLEANING.", ModuleName, "Event");
            }
            else if ((Define.seqBrushFwBwMode == Define.MODE_BRUSH_FWBW_CLEAN) && (Define.seqBrushFwBwCtrl == Define.CTRL_RUNNING))
            {
                switch (step.Layer)
                {
                    case 1:
                        {
                            P_BRUSH_CYLINDER_FwdBwd_Home();
                        }
                        break;

                    case 2:
                        {
                            P_BRUSH_UpDn_Seq("Down");
                        }
                        break;

                    case 3:
                        {
                            P_BRUSH_Rotation("Run");
                        }
                        break;

                    case 4:
                        {
                            P_BRUSH_Air_Water_Setting("Open");
                        }
                        break;

                    case 5:
                        {
                            P_BRUSH_Clean_Timecheck(Configure_List.Brush_Clean_Time);
                        }
                        break;

                    case 6:
                        {
                            P_BRUSH_Air_Water_Setting("Close");
                        }
                        break;

                    case 7:
                        {
                            P_BRUSH_Rotation("Stop");
                        }
                        break;

                    case 8:
                        {
                            P_BRUSH_CleanEnd();
                        }
                        break;
                }
            }
        }
        #endregion
        /////////////////////////////////////////////////////////////////////////////////////
        ///
        // FUNCTION /////////////////////////////////////////////////////////////////////////
        #region FUNCTION
        private void P_BRUSH_CYLINDER_FwdBwd(string FwdBwd)
        {
            if (step.Flag)
            {
                Global.EventLog("Brush cylinder : " + FwdBwd, ModuleName, "Event");

                Global.SetDigValue((int)DigOutputList.CH1_Brush_Pwr_o, (uint)DigitalOffOn.On, ModuleName);

                if (FwdBwd == "Forward")
                {
                    if (Global.GetDigValue((int)DigInputList.CH1_Brush_Fwd_i) == "Off")
                    {
                        F_INC_STEP();
                    }
                    else
                    {
                        Global.SetDigValue((int)DigOutputList.CH1_Brush_Fwd_o, (uint)DigitalOffOn.On, ModuleName);
                        Global.SetDigValue((int)DigOutputList.CH1_Brush_Bwd_o, (uint)DigitalOffOn.Off, ModuleName);

                        step.Flag = false;
                        step.Times = 1;
                    }                    
                }
                else if (FwdBwd == "Backward")
                {
                    if (Global.GetDigValue((int)DigInputList.CH1_Brush_Bwd_i) == "Off")
                    {
                        F_INC_STEP();
                    }
                    else
                    {
                        Global.SetDigValue((int)DigOutputList.CH1_Brush_Fwd_o, (uint)DigitalOffOn.Off, ModuleName);
                        Global.SetDigValue((int)DigOutputList.CH1_Brush_Bwd_o, (uint)DigitalOffOn.On, ModuleName);

                        step.Flag = false;
                        step.Times = 1;
                    }                    
                }                
            }
            else
            {
                if (FwdBwd == "Forward")
                {
                    if (Global.GetDigValue((int)DigInputList.CH1_Brush_Fwd_i) == "Off")
                    {
                        Global.SetDigValue((int)DigOutputList.CH1_Brush_Fwd_o, (uint)DigitalOffOn.Off, ModuleName);
                        //Thread.Sleep(500);
                        Task.Delay(500);

                        F_INC_STEP();
                    }
                    else
                    {
                        if (step.Times >= Configure_List.Brush_FwdBwd_Timeout)
                        {
                            ShowAlarm("1030");
                        }
                        else
                        {
                            step.INC_TIMES_10();
                        }
                    }
                }
                else
                {
                    if (Global.GetDigValue((int)DigInputList.CH1_Brush_Bwd_i) == "Off")
                    {
                        Global.SetDigValue((int)DigOutputList.CH1_Brush_Bwd_o, (uint)DigitalOffOn.Off, ModuleName);
                        //Thread.Sleep(500);
                        Task.Delay(500);

                        F_INC_STEP();
                    }
                    else
                    {
                        if (step.Times >= Configure_List.Brush_FwdBwd_Timeout)
                        {
                            ShowAlarm("1031");
                        }
                        else
                        {
                            step.INC_TIMES_10();
                        }
                    }
                }
            }
        }

        private void P_BRUSH_CYLINDER_StepCheck(byte nStep)
        {
            if (step.Flag)
            {
                step.Flag = false;
                step.Times = 1;
            }
            else
            {
                step.Flag = true;
                step.Layer = nStep;
            }
        }

        private void P_BRUSH_CYLINDER_FwdBwd_Home()
        {
            if (step.Flag)
            {
                if (Global.GetDigValue((int)DigInputList.CH1_Brush_Home_i) == "Off")
                {
                    F_INC_STEP();
                }
                else
                {
                    Global.SetDigValue((int)DigOutputList.CH1_Brush_Pwr_o, (uint)DigitalOffOn.On, ModuleName);

                    Global.SetDigValue((int)DigOutputList.CH1_Brush_Fwd_o, (uint)DigitalOffOn.Off, ModuleName);
                    Global.SetDigValue((int)DigOutputList.CH1_Brush_Bwd_o, (uint)DigitalOffOn.On, ModuleName);

                    step.Flag = false;
                    step.Times = 1;
                }                    
            }
            else
            {
                if (Global.GetDigValue((int)DigInputList.CH1_Brush_Home_i) == "Off")
                {                    
                    Global.SetDigValue((int)DigOutputList.CH1_Brush_Bwd_o, (uint)DigitalOffOn.Off, ModuleName);
                    //Thread.Sleep(500);
                    Task.Delay(500);

                    F_INC_STEP();
                }
                else
                {
                    if (step.Times >= Configure_List.Brush_FwdBwd_Timeout)
                    {
                        ShowAlarm("1032");
                    }
                    else
                    {
                        step.INC_TIMES_10();
                    }
                }
            }
        }

        private void P_BRUSH_CYLINDER_FwdBwd_HomeEnd()
        {
            Define.seqBrushFwBwMode = Define.MODE_BRUSH_FWBW_IDLE;
            Define.seqBrushFwBwCtrl = Define.CTRL_IDLE;
            Define.seqBrushFwBwSts = Define.STS_BRUSH_FWBW_HOMEEND;            

            Global.EventLog("COMPLETE THE BRUSH CYLINDER HOME.", ModuleName, "Event");           
        }

        private void P_BRUSH_UpDn_Seq(string UpDn)
        {
            if (step.Flag)
            {
                if (UpDn == "Up")
                {
                    Define.seqBrushUpDnMode = Define.MODE_BRUSH_UPDN_UP;
                    Define.seqBrushUpDnCtrl = Define.CTRL_RUN;
                    Define.seqBrushUpDnSts = Define.STS_BRUSH_UPDN_IDLE;
                }
                else
                {
                    Define.seqBrushUpDnMode = Define.MODE_BRUSH_UPDN_DOWN;
                    Define.seqBrushUpDnCtrl = Define.CTRL_RUN;
                    Define.seqBrushUpDnSts = Define.STS_BRUSH_UPDN_IDLE;
                }

                step.Flag = false;
                step.Times = 1;
            }
            else
            {
                if (UpDn == "Up")
                {
                    if (step.Times > 1)
                    {
                        if ((Define.seqBrushUpDnCtrl == Define.CTRL_IDLE) &&
                            (Define.seqBrushUpDnSts == Define.STS_BRUSH_UPDN_UPEND))
                        {
                            F_INC_STEP();
                        }
                        else
                        {
                            step.INC_TIMES();
                        }
                    }
                    else
                    {
                        step.INC_TIMES();
                    }
                }
                else
                {
                    if (step.Times > 1)
                    {
                        if ((Define.seqBrushUpDnCtrl == Define.CTRL_IDLE) &&
                            (Define.seqBrushUpDnSts == Define.STS_BRUSH_UPDN_DOWNEND))
                        {
                            F_INC_STEP();
                        }
                        else
                        {
                            step.INC_TIMES();
                        }
                    }
                    else
                    {
                        step.INC_TIMES();
                    }
                }
            }
        }

        private void P_BRUSH_Rotation(string sAction)
        {
            if (step.Flag)
            {
                if (sAction == "Run")
                {
                    double dVel = Configure_List.Brush_Rotation_Speed;
                    double dAcc = dVel * 2;
                    double dDec = dVel * 2;

                    MotionClass.MotorJogP(Define.axis_r, dVel, dAcc, dDec);
                }
                else
                {
                    MotionClass.SetMotorSStop(Define.axis_r);
                }

                step.Flag = false;
                step.Times = 1;
            }
            else
            {
                if (step.Times > 1)
                {
                    if (sAction == "Run")
                    {
                        if (MotionClass.motor[Define.axis_r].sR_BusyStatus == "Moving")
                        {
                            F_INC_STEP();
                        }
                        else
                        {
                            if (step.Times >= Configure_List.Brush_Rotation_Timeout)
                            {
                                ShowAlarm("1045");
                            }
                            else
                            {
                                step.INC_TIMES_10();
                            }
                        }
                    }
                    else
                    {
                        if (MotionClass.motor[Define.axis_r].sR_BusyStatus == "Ready")
                        {
                            F_INC_STEP();
                        }
                        else
                        {
                            if (step.Times >= Configure_List.Brush_Rotation_Timeout)
                            {
                                ShowAlarm("1046");
                            }
                            else
                            {
                                step.INC_TIMES_10();
                            }
                        }
                    }
                }
                else
                {
                    step.INC_TIMES();
                }
            }
        }

        private void P_BRUSH_Air_Water_Setting(string OpCl)
        {
            if (step.Flag)
            {
                if (OpCl == "Open")
                {
                    // Air
                    Global.SetDigValue((int)DigOutputList.CH1_BrushClean_AirValve_o, (uint)DigitalOffOn.On, ModuleName);                    

                    // Water
                    Global.SetDigValue((int)DigOutputList.CH1_BrushClean_WaterValve_o, (uint)DigitalOffOn.On, ModuleName);
                }
                else
                {
                    // Air
                    Global.SetDigValue((int)DigOutputList.CH1_BrushClean_AirValve_o, (uint)DigitalOffOn.Off, ModuleName);

                    // Water
                    Global.SetDigValue((int)DigOutputList.CH1_BrushClean_WaterValve_o, (uint)DigitalOffOn.Off, ModuleName);
                }

                step.Flag = false;
                step.Times = 1;
            }
            else
            {
                F_INC_STEP();
            }
        }

        private void P_BRUSH_Clean_Timecheck(int iTime)
        {
            if (step.Flag)
            {
                Global.EventLog("Check the brush clean time : " + iTime.ToString() + "sec.", ModuleName, "Event");

                step.Flag = false;
                step.Times = 1;
            }
            else
            {
                if (step.Times >= iTime)
                {
                    F_INC_STEP();
                }
                else
                {
                    step.INC_TIMES();
                }
            }
        }

        private void P_BRUSH_CleanEnd()
        {
            Define.seqBrushFwBwMode = Define.MODE_BRUSH_FWBW_IDLE;
            Define.seqBrushFwBwCtrl = Define.CTRL_IDLE;
            Define.seqBrushFwBwSts = Define.STS_BRUSH_FWBW_CLEANEND;

            Global.EventLog("COMPLETE THE BRUSH CLEAN.", ModuleName, "Event");
        }

        private void F_PROCESS_ALL_VALVE_CLOSE()
        {            
            // Air
            Global.SetDigValue((int)DigOutputList.CH1_BrushClean_AirValve_o, (uint)DigitalOffOn.Off, ModuleName);            

            // Water
            Global.SetDigValue((int)DigOutputList.CH1_BrushClean_WaterValve_o, (uint)DigitalOffOn.Off, ModuleName);            
        }
        #endregion
        /////////////////////////////////////////////////////////////////////////////////////
    }
}
