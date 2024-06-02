using Ajin_motion_driver;
using MsSqlManagerLibrary;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PKGSawKit_CleanerSystem_New.Squence
{
    class PM2Cylinder : TBaseThread
    {
        Thread thread;
        private new TStep step;
        Alarm_List alarm_List;  // Alarm list
        
        int nHomeCnt;

        public PM2Cylinder()
        {
            ModuleName = "PM2";
            module = (byte)MODULE._PM2;
            
            thread = new Thread(new ThreadStart(Execute));
            
            alarm_List = new Alarm_List();

            Define.bHomeFlag = false;

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
                    if (Define.seqCylinderCtrl[module] == Define.CTRL_ABORT)
                    {
                        AlarmAction("Abort");
                    }
                    else if (Define.seqCylinderCtrl[module] == Define.CTRL_RETRY)
                    {
                        AlarmAction("Retry");
                    }

                    Run_Progress();
                    Home_Progress();

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

                Define.seqCylinderCtrl[module] = Define.CTRL_RUNNING;

                if (Define.seqCtrl[module] == Define.CTRL_ALARM)
                {
                    Define.seqCtrl[module] = Define.CTRL_RUNNING;
                }
            }
            else if (sAction == "Abort")
            {
                ActionList();
                
                Define.seqCylinderMode[module] = Define.MODE_CYLINDER_IDLE;
                Define.seqCylinderCtrl[module] = Define.CTRL_IDLE;
                Define.seqCylinderSts[module] = Define.STS_CYLINDER_ABORTOK;

                step.Times = 1;                

                Global.EventLog("Cylinder movement stopped : " + sAction, ModuleName, "Event");
            }
        }

        private void ActionList()
        {
            F_PROCESS_ALL_VALVE_CLOSE();

            MotionClass.SetMotorSStop(Define.axis_y);
        }

        private void ShowAlarm(string almId)
        {
            ActionList();

            Define.seqCylinderCtrl[module] = Define.CTRL_ALARM;

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

        private void F_PROCESS_ALL_VALVE_CLOSE()
        {
            // Air
            Global.SetDigValue((int)DigOutputList.CH2_AirValve_1_o, (uint)DigitalOffOn.Off, ModuleName);
            Global.SetDigValue((int)DigOutputList.CH2_AirValve_2_o, (uint)DigitalOffOn.Off, ModuleName);

            // Water
            Global.SetDigValue((int)DigOutputList.CH2_WaterValve_1_o, (uint)DigitalOffOn.Off, ModuleName);
            Global.SetDigValue((int)DigOutputList.CH2_WaterValve_2_o, (uint)DigitalOffOn.Off, ModuleName);
            Global.SetDigValue((int)DigOutputList.CH2_WaterValve_3_o, (uint)DigitalOffOn.Off, ModuleName);
            Global.SetDigValue((int)DigOutputList.CH2_WaterValve_4_o, (uint)DigitalOffOn.Off, ModuleName);
            Global.SetDigValue((int)DigOutputList.CH2_WaterValve_5_o, (uint)DigitalOffOn.Off, ModuleName);
        }

        // CYLINDER PROGRESS ////////////////////////////////////////////////////////////////
        #region CYLINDER_PROGRESS
        private void Run_Progress()
        {
            if ((Define.seqCylinderMode[module] == Define.MODE_CYLINDER_RUN) && (Define.seqCylinderCtrl[module] == Define.CTRL_RUN))
            {
                step.Layer = 1;
                step.Times = 1;
                step.Flag = true;

                Define.seqCylinderCtrl[module] = Define.CTRL_RUNNING;
                Define.seqCylinderSts[module] = Define.STS_CYLINDER_RUNING;                

                Global.EventLog("START THE CYLINDER MOVING.", ModuleName, "Event");
            }
            else if ((Define.seqCylinderMode[module] == Define.MODE_CYLINDER_RUN) && (Define.seqCylinderCtrl[module] == Define.CTRL_RUNNING))
            {
                switch (step.Layer)
                {
                    case 1:
                        {
                            P_CYLINDER_FwdBwd_Home();
                        }
                        break;

                    case 2:
                        {
                            P_CYLINDER_FwdBwd("Forward");
                        }
                        break;

                    case 3:
                        {
                            P_CYLINDER_Delay(1);
                        }
                        break;

                    case 4:
                        {
                            P_CYLINDER_FwdBwd("Backward");
                        }
                        break;

                    case 5:
                        {
                            P_CYLINDER_Delay(1);
                        }
                        break;

                    case 6:
                        {
                            P_CYLINDER_StepCheck(2);
                        }
                        break;                    
                }
            }
        }

        private void Home_Progress()
        {
            if ((Define.seqCylinderMode[module] == Define.MODE_CYLINDER_HOME) && (Define.seqCylinderCtrl[module] == Define.CTRL_RUN))
            {
                Thread.Sleep(500);
                step.Layer = 1;
                step.Times = 1;
                step.Flag = true;

                Define.bHomeFlag = false;

                Define.seqCylinderCtrl[module] = Define.CTRL_RUNNING;
                Define.seqCylinderSts[module] = Define.STS_CYLINDER_HOMEING;                

                Global.EventLog("START THE CYLINDER HOME.", ModuleName, "Event");
            }
            else if ((Define.seqCylinderMode[module] == Define.MODE_CYLINDER_HOME) && (Define.seqCylinderCtrl[module] == Define.CTRL_RUNNING))
            {
                switch (step.Layer)
                {
                    case 1:
                        {
                            P_CYLINDER_FwdBwd_Home();
                        }
                        break;

                    case 2:
                        {
                            P_CYLINDER_FwdBwd_HomeEnd();
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
        private void P_CYLINDER_FwdBwd(string FwdBwd)
        {
            if (step.Flag)
            {
                Global.EventLog("Cylinder : " + FwdBwd, ModuleName, "Event");

                MotionClass.SetMotorVelocity(Define.axis_y, Configure_List.WaterBlock_Move_Speed);
                MotionClass.SetMotorAccel(Define.axis_y, Configure_List.WaterBlock_Move_Speed * 2);
                MotionClass.SetMotorDecel(Define.axis_y, Configure_List.WaterBlock_Move_Speed * 2);
                MotionClass.SetMotorGearing(Define.axis_y, 1);

                Thread.Sleep(500);

                if (FwdBwd == "Forward")
                {
                    if ((MotionClass.motor[Define.axis_y].sR_HomeStatus == "+Limit") &&
                        (MotionClass.motor[Define.axis_y].sR_BusyStatus == "Ready"))
                    {                        
                        F_INC_STEP();
                    }
                    else
                    {                                                
                        if (Global.MOTION_INTERLOCK_CHECK())
                        {                            
                            MotionClass.MotorMove(Define.axis_y, Configure_List.WaterBlock_Fwd_Position);                            
                        }
                        
                        step.Flag = false;
                        step.Times = 1;
                    }                    
                }
                else if (FwdBwd == "Backward")
                {
                    if ((MotionClass.motor[Define.axis_y].sR_HomeStatus == "Home") &&
                        (MotionClass.motor[Define.axis_y].sR_BusyStatus == "Ready"))
                    {                       
                        F_INC_STEP();
                    }
                    else
                    {                                               
                        if (Global.MOTION_INTERLOCK_CHECK())
                        {                                                        
                            MotionClass.MotorMove(Define.axis_y, Configure_List.WaterBlock_Bwd_Position);
                        }                        

                        step.Flag = false;
                        step.Times = 1;
                    }                    
                }                
            }
            else
            {                
                if (FwdBwd == "Forward")
                {
                    if ((MotionClass.motor[Define.axis_y].sR_HomeStatus == "+Limit") &&
                        (MotionClass.motor[Define.axis_y].sR_BusyStatus == "Ready"))
                    {                        
                        F_INC_STEP();
                    }
                    else if (MotionClass.motor[Define.axis_y].sR_AlarmStatus == "Alarm")
                    {
                        MotionClass.SetAlarmReset(Define.axis_y);

                        Thread.Sleep(500);

                        if (step.Times >= 5)
                        {
                            step.Flag = true;
                        }
                        else
                        {
                            step.INC_TIMES();
                        }
                    }
                    else
                    {
                        if (step.Times >= Configure_List.Nozzle_FwdBwd_Timeout)
                        {
                            ShowAlarm("1020");
                        }
                        else
                        {
                            step.INC_TIMES_10();
                        }
                    }
                }
                else
                {
                    if ((MotionClass.motor[Define.axis_y].sR_HomeStatus == "Home") &&
                        (MotionClass.motor[Define.axis_y].sR_BusyStatus == "Ready"))
                    {                        
                        F_INC_STEP();
                    }
                    else if (MotionClass.motor[Define.axis_y].sR_AlarmStatus == "Alarm")
                    {
                        MotionClass.SetAlarmReset(Define.axis_y);

                        Thread.Sleep(500);

                        if (step.Times >= 5)
                        {
                            step.Flag = true;
                        }
                        else
                        {
                            step.INC_TIMES();
                        }
                    }
                    else
                    {
                        if (step.Times >= Configure_List.Nozzle_FwdBwd_Timeout)
                        {
                            ShowAlarm("1021");
                        }
                        else
                        {
                            step.INC_TIMES_10();
                        }
                    }
                }
            }
        }        

        private void P_CYLINDER_FwdBwd_Home()
        {
            if (step.Flag)
            {
                if (Define.seqCylinderMode[module] == Define.MODE_CYLINDER_HOME)
                {
                    MotionClass.SetMotorSStop(Define.axis_y);

                    Thread.Sleep(1000);

                    if (Global.MOTION_INTERLOCK_CHECK())
                    {
                        MotionClass.SetMotorHome(Define.axis_y);
                    }

                    step.Flag = false;
                    step.Times = 1;

                    nHomeCnt = 0;
                }
                else
                {
                    if (Define.bHomeFlag)
                    {
                        F_INC_STEP();
                    }
                    else
                    {
                        MotionClass.SetMotorSStop(Define.axis_y);

                        Thread.Sleep(1000);

                        if (Global.MOTION_INTERLOCK_CHECK())
                        {
                            MotionClass.SetMotorHome(Define.axis_y);
                        }

                        step.Flag = false;
                        step.Times = 1;

                        nHomeCnt = 0;
                    }
                }                                
            }
            else
            {
                if (step.Times > 20)
                {
                    if ((MotionClass.motor[Define.axis_y].sR_HomeStatus == "Home") &&
                        (MotionClass.motor[Define.axis_y].sR_BusyStatus == "Ready"))
                    {                        
                        Define.bHomeFlag = true;

                        if (nHomeCnt > 5)
                        {
                            F_INC_STEP();
                        }
                        else
                        {
                            nHomeCnt++;
                        }
                    }
                    else if (MotionClass.motor[Define.axis_y].sR_AlarmStatus == "Alarm")
                    {
                        MotionClass.SetAlarmReset(Define.axis_y);

                        Thread.Sleep(500);

                        if (step.Times >= 5)
                        {
                            step.Flag = true;
                        }                            
                        else
                        {
                            step.INC_TIMES();
                        }

                        nHomeCnt = 0;
                    }
                    else
                    {
                        if (step.Times >= Configure_List.Nozzle_FwdBwd_Timeout)
                        {
                            ShowAlarm("1022");
                        }
                        else
                        {
                            step.INC_TIMES_10();
                        }

                        nHomeCnt = 0;
                    }
                }
                else
                {
                    step.INC_TIMES();
                }                
            }
        }

        private void P_CYLINDER_FwdBwd_HomeEnd()
        {
            Define.seqCylinderMode[module] = Define.MODE_CYLINDER_IDLE;
            Define.seqCylinderCtrl[module] = Define.CTRL_IDLE;
            Define.seqCylinderSts[module] = Define.STS_CYLINDER_HOMEEND;            

            Global.EventLog("COMPLETE THE CYLINDER HOME.", ModuleName, "Event");            
        }

        private void P_CYLINDER_StepCheck(byte nStep)
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

        private void P_CYLINDER_Delay(int nTime)
        {
            if (step.Flag)
            {
                step.Flag = false;
                step.Times = 1;
            }
            else
            {
                if (step.Times > nTime)
                {
                    F_INC_STEP();
                }
                else
                {
                    step.INC_TIMES();
                }                
            }
        }
        #endregion
        /////////////////////////////////////////////////////////////////////////////////////
    }
}
