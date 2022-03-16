using System;
using System.Collections.Generic;
using System.Text;

namespace BackaBarnet
{
    public class Person
    {
        public string PersonNbr { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Guardian1 { get; set; }
        public string GuardianPersonNbr1 { get; set; }
        public string Guardian2 { get; set; }
        public string GuardianPersonNbr2 { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedOn { get; set; }
        public string Status { get; set; }
        public int PersonID { get; set; }
    }

    public class User
    {
        public int UserID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public string Description { get; set; }
        public string Organisaton { get; set; }
        public string Name { get; set; }
        public int UnitID { get; set; }
        public string UnitName { get; set; }

    }

    public class Contact
    {
        public string ContactPersonNbr { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNbr { get; set; }
        public string Employer { get; set; }
    }

    public class Card
    {
        public DateTime GradedOn { get; set; }
        public string UserName { get; set; }
        public string UserOrg { get; set; }
        public string UserTitle { get; set; }
        public string PersonName { get; set; }
        public string PersonNbr { get; set; }
        public string Guardian1 { get; set; }
        public string GuardianPersonNbr1 { get; set; }
        public string Guardian2 { get; set; }
        public string GuardianPersonNbr2 { get; set; }
        public string Unit { get; set; }
        public string Situation { get; set; }
        public int QuestionID { get; set; }
        public int Grade { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public int PersonID { get; set; }
    }

    public class Unit
    {
        public int UnitID { get; set; }
        public string UnitName { get; set; }
    }

    public class Body
    {
        public int UserID { get; set; }
        public string PersonLastName { get; set; }
        public string PersonFirstName { get; set; }
        public string PersonNbr { get; set; }
        public string GuardianName1 { get; set; }
        public string GuardianNbr1 { get; set; }
        public string GuardianName2 { get; set; }
        public string GuardianNbr2 { get; set; }
        public string Unit { get; set; }
        public string Situation { get; set; }
        public int GradeActions { get; set; }
        public string CommentActions { get; set; }
        public int GradeOmsorg { get; set; }
        public string CommentOmsorg { get; set; }
        public int GradeTrygghet { get; set; }
        public string CommentTrygghet { get; set; }
        public int GradeMarBra { get; set; }
        public string CommentMarBra { get; set; }
        public int GradeFritid { get; set; }
        public string CommentFritid { get; set; }
        public int GradeTillhorighet { get; set; }
        public string CommentTillhorighet { get; set; }
        public int GradeAnsvarstagande { get; set; }
        public string CommentAnsvarstagande { get; set; }
        public int GradeRespekteras { get; set; }
        public string CommentRespekteras { get; set; }
        public int GradeUtvecklas { get; set; }
        public string CommentUtvecklas { get; set; }
        public int GradeUpprattats1 { get; set; }
        public int GradeUpprattats2 { get; set; }
        public int GradeSamtycke1 { get; set; }
        public int GradeSamtycke2 { get; set; }
        public int Status { get; set; }
        public string GradedOn { get; set; }
        public int PersonID { get; set; }
        public int GradeOmsorg1 { get; set; }
        public string CommentOmsorg1 { get; set; }
        public int GradeTrygghet1 { get; set; }
        public string CommentTrygghet1 { get; set; }
        public int GradeMarBra1 { get; set; }
        public string CommentMarBra1 { get; set; }
        public int GradeFritid1 { get; set; }
        public string CommentFritid1 { get; set; }
        public int GradeTillhorighet1 { get; set; }
        public string CommentTillhorighet1 { get; set; }
        public int GradeAnsvarstagande1 { get; set; }
        public string CommentAnsvarstagande1 { get; set; }
        public int GradeRespekteras1 { get; set; }
        public string CommentRespekteras1 { get; set; }
        public int GradeUtvecklas1 { get; set; }
        public string CommentUtvecklas1 { get; set; }
        public int GradeOmsorg2 { get; set; }
        public string CommentOmsorg2 { get; set; }
        public int GradeTrygghet2 { get; set; }
        public string CommentTrygghet2 { get; set; }
        public int GradeMarBra2 { get; set; }
        public string CommentMarBra2 { get; set; }
        public int GradeFritid2 { get; set; }
        public string CommentFritid2 { get; set; }
        public int GradeTillhorighet2 { get; set; }
        public string CommentTillhorighet2 { get; set; }
        public int GradeAnsvarstagande2 { get; set; }
        public string CommentAnsvarstagande2 { get; set; }
        public int GradeRespekteras2 { get; set; }
        public string CommentRespekteras2 { get; set; }
        public int GradeUtvecklas2 { get; set; }
        public string CommentUtvecklas2 { get; set; }
        public int StatusID { get; set; }
        public int GradeOmsorg3 { get; set; }
        public int GradeOmsorg4 { get; set; }
        public int GradeOmsorg5 { get; set; }
        public int GradeOmsorg6 { get; set; }
        public int GradeTrygghet3 { get; set; }
        public int GradeTrygghet4 { get; set; }
        public int GradeTrygghet5 { get; set; }
        public int GradeTrygghet6 { get; set; }
        public int GradeTrygghet7 { get; set; }
        public int GradeTrygghet8 { get; set; }
        public int GradeMarBra3 { get; set; }
        public int GradeMarBra4 { get; set; }
        public int GradeMarBra5 { get; set; }
        public int GradeMarBra6 { get; set; }
        public int GradeFritid3 { get; set; }
        public int GradeTillhorighet3 { get; set; }
        public int GradeAnsvarstagande3 { get; set; }
        public int GradeAnsvarstagande4 { get; set; }
        public int GradeAnsvarstagande5 { get; set; }
        public int GradeAnsvarstagande6 { get; set; }
        public int GradeAnsvarstagande7 { get; set; }
        public int GradeRespekteras3 { get; set; }
        public int GradeRespekteras4 { get; set; }
        public int GradeUtvecklas3 { get; set; }
        public int GradeUtvecklas4 { get; set; }
        public int GradeUtvecklas5 { get; set; }
        public string Action { get; set; }
        public int EventID { get; set; }
        public string Responsible { get; set; }
        public string Profession { get; set;  }
        public string Event { get; set; }
        public int ActionID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Organisation { get; set; }
        public int RoleID { get; set; }
        public int UnitID { get; set; }
        public string TeamName { get; set; }
        public int TeamID { get; set; }


    }

    public class Error
    {
        public int ErrorNumber { get; set; }
        public int ErrorSeverity { get; set; }
        public int ErrorState { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Substrate
    {
        public int QuestionID { get; set; }
        public int PersonID { get; set; }
        public int Grade { get; set; }
        public string Comment { get; set; }
        public string GradeType { get; set; }
        public DateTime GradedOn { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
    }

    public class Status
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }

    public class Estimate
    {
        public int QuestionID { get; set; }
        public int PersonID { get; set; }
        public int UserID { get; set; }
        public int Grade { get; set; }
        public string Comment { get; set; }
        public DateTime GradedOn { get; set; }
        public DateTime ChangedOn { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int QuestionLevelID { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
    }

    public class Compass
    {
        public int QuestionID { get; set; }
        public int QuestionTypeID { get; set; }
        public int QuestionLevelID { get; set; }
        public int PersonID { get; set; }
        public int UserID { get; set; }
        public int Grade { get; set; }
        public string Comment { get; set; }
        public DateTime GradedOn { get; set; }
        public string UserName { get; set; }
    }

    public class Event
    {
        public int ActionID { get; set; }
        public string ActionDescription { get; set; }
        public int EventID { get; set; }
        public string EventDescription { get; set; }
        public int PersonID { get; set; }
        public string Responsible { get; set; }
        public string Profession { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedOn { get; set; }
        public int UnitID { get; set; }
        public string UnitName { get; set; }
    }

    public class UserRight
    {
        public int CurrentUserID { get; set; }
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public int Type { get; set; }
    }

    public class ReturnedUserRight
    {
        public string CurrentUserID { get; set; }
        public string UserID { get; set; }
        public string UserLastName { get; set; }
        public string UserFirstName { get; set; }
        public string QuestionTypeID { get; set; }
        public string PersonID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

    }

}
