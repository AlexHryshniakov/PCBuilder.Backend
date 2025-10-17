namespace PCBuilder.Core.Enums;

public enum Permission
{
    ReadPcItems= 1,//User,Manager,Master,Admin
    CreatePcItems= 2,//Manager,Admin
    UpdatePcItems= 3,//Manager,Admin
    DeletePcItems= 4,//Manager,Admin
    
    ReadPc= 5,//User,Master,Manager,Admin
    CreatePc= 6,//Master,Admin
    UpdatePc= 7,//Master,Admin
    DeletePc= 8,//Master,Admin
    
    ScheduleDate=9,//Manager,User,Admin
    CancelScheduleDate=10,//Manager,Master,User,Admin
    RescheduleDate=11,//Manager,Master,User,Admin
    
    GetUsersScheduleDate=12,//User,Manager,Admin
    GetMastersScheduleDate=13,//Master,User,Manager,Admin
    GetAllScheduleDates=14,//Manager,Admin
    
    PayOrder=15,//User,Admin
    GetUsersOrders=16,//User,Manager,Admin
    GetMastersOrders=17,//Master,Manager,Admin
    GetAllOrders=18,//Manager,Admin
}