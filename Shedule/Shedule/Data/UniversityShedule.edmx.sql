
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/07/2013 18:34:28
-- Generated from EDMX file: C:\Users\nick\git\Shedule\Shedule\Data\UniversityShedule.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [shedule];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AuditoriumDepartment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Auditoriums] DROP CONSTRAINT [FK_AuditoriumDepartment];
GO
IF OBJECT_ID(N'[dbo].[FK_LessonsTypeLessonsSubType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LessonsSubTypes] DROP CONSTRAINT [FK_LessonsTypeLessonsSubType];
GO
IF OBJECT_ID(N'[dbo].[FK_FacultyDepartment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Departments] DROP CONSTRAINT [FK_FacultyDepartment];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployePosition_Employe]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployePosition] DROP CONSTRAINT [FK_EmployePosition_Employe];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployePosition_Position]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployePosition] DROP CONSTRAINT [FK_EmployePosition_Position];
GO
IF OBJECT_ID(N'[dbo].[FK_LessonRing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lessons] DROP CONSTRAINT [FK_LessonRing];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentRegulatoryAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RegulatoryActions] DROP CONSTRAINT [FK_DepartmentRegulatoryAction];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupStudyType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Groups] DROP CONSTRAINT [FK_GroupStudyType];
GO
IF OBJECT_ID(N'[dbo].[FK_TestsPeriodGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestsPeriods] DROP CONSTRAINT [FK_TestsPeriodGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_TestsPeriodSessionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestsPeriods] DROP CONSTRAINT [FK_TestsPeriodSessionType];
GO
IF OBJECT_ID(N'[dbo].[FK_LessonRegulatoryAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lessons] DROP CONSTRAINT [FK_LessonRegulatoryAction];
GO
IF OBJECT_ID(N'[dbo].[FK_LessonAuditorium]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lessons] DROP CONSTRAINT [FK_LessonAuditorium];
GO
IF OBJECT_ID(N'[dbo].[FK_TitleEmploye]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_TitleEmploye];
GO
IF OBJECT_ID(N'[dbo].[FK_AcademicLoadEmploye]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AcademicLoadSet] DROP CONSTRAINT [FK_AcademicLoadEmploye];
GO
IF OBJECT_ID(N'[dbo].[FK_AcademicLoadRegulatoryAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AcademicLoadSet] DROP CONSTRAINT [FK_AcademicLoadRegulatoryAction];
GO
IF OBJECT_ID(N'[dbo].[FK_CurriculumRegulatoryAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Curriculums] DROP CONSTRAINT [FK_CurriculumRegulatoryAction];
GO
IF OBJECT_ID(N'[dbo].[FK_CurriculumSubject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Curriculums] DROP CONSTRAINT [FK_CurriculumSubject];
GO
IF OBJECT_ID(N'[dbo].[FK_CurriculumGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Curriculums] DROP CONSTRAINT [FK_CurriculumGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_TestCurriculumEmploye]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestCurriculums] DROP CONSTRAINT [FK_TestCurriculumEmploye];
GO
IF OBJECT_ID(N'[dbo].[FK_TestActionSubject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestActions] DROP CONSTRAINT [FK_TestActionSubject];
GO
IF OBJECT_ID(N'[dbo].[FK_TestActionGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestActions] DROP CONSTRAINT [FK_TestActionGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_TestActionTestsType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestActions] DROP CONSTRAINT [FK_TestActionTestsType];
GO
IF OBJECT_ID(N'[dbo].[FK_TestActionDepartment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestActions] DROP CONSTRAINT [FK_TestActionDepartment];
GO
IF OBJECT_ID(N'[dbo].[FK_TestCurriculumTestAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestCurriculums] DROP CONSTRAINT [FK_TestCurriculumTestAction];
GO
IF OBJECT_ID(N'[dbo].[FK_FacultyGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Groups] DROP CONSTRAINT [FK_FacultyGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_FieldOfStudyGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Groups] DROP CONSTRAINT [FK_FieldOfStudyGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_RegulatoryActionLessonsType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RegulatoryActions] DROP CONSTRAINT [FK_RegulatoryActionLessonsType];
GO
IF OBJECT_ID(N'[dbo].[FK_ValidationTestAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Validations] DROP CONSTRAINT [FK_ValidationTestAction];
GO
IF OBJECT_ID(N'[dbo].[FK_ValidationRing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Validations] DROP CONSTRAINT [FK_ValidationRing];
GO
IF OBJECT_ID(N'[dbo].[FK_ValidationAuditorium]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Validations] DROP CONSTRAINT [FK_ValidationAuditorium];
GO
IF OBJECT_ID(N'[dbo].[FK_ActivityRing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Activities] DROP CONSTRAINT [FK_ActivityRing];
GO
IF OBJECT_ID(N'[dbo].[FK_ActivityAuditorium]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Activities] DROP CONSTRAINT [FK_ActivityAuditorium];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeFaculty]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_EmployeFaculty];
GO
IF OBJECT_ID(N'[dbo].[FK_EduPeriodGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EduPeriods] DROP CONSTRAINT [FK_EduPeriodGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_DegreeEmploye]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_DegreeEmploye];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Auditoriums]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Auditoriums];
GO
IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[LessonsTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LessonsTypes];
GO
IF OBJECT_ID(N'[dbo].[LessonsSubTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LessonsSubTypes];
GO
IF OBJECT_ID(N'[dbo].[Rings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rings];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[Faculties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Faculties];
GO
IF OBJECT_ID(N'[dbo].[TestsTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TestsTypes];
GO
IF OBJECT_ID(N'[dbo].[FieldsOfStudy]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FieldsOfStudy];
GO
IF OBJECT_ID(N'[dbo].[SessionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SessionTypes];
GO
IF OBJECT_ID(N'[dbo].[StudyTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StudyTypes];
GO
IF OBJECT_ID(N'[dbo].[Positions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Positions];
GO
IF OBJECT_ID(N'[dbo].[Subjects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Subjects];
GO
IF OBJECT_ID(N'[dbo].[Degrees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Degrees];
GO
IF OBJECT_ID(N'[dbo].[Titles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Titles];
GO
IF OBJECT_ID(N'[dbo].[RegulatoryActions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RegulatoryActions];
GO
IF OBJECT_ID(N'[dbo].[Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Groups];
GO
IF OBJECT_ID(N'[dbo].[EduPeriods]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EduPeriods];
GO
IF OBJECT_ID(N'[dbo].[TestsPeriods]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TestsPeriods];
GO
IF OBJECT_ID(N'[dbo].[Lessons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Lessons];
GO
IF OBJECT_ID(N'[dbo].[Validations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Validations];
GO
IF OBJECT_ID(N'[dbo].[Activities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Activities];
GO
IF OBJECT_ID(N'[dbo].[AcademicLoadSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AcademicLoadSet];
GO
IF OBJECT_ID(N'[dbo].[Curriculums]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Curriculums];
GO
IF OBJECT_ID(N'[dbo].[TestCurriculums]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TestCurriculums];
GO
IF OBJECT_ID(N'[dbo].[TestActions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TestActions];
GO
IF OBJECT_ID(N'[dbo].[EmployePosition]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployePosition];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Auditoriums'
CREATE TABLE [dbo].[Auditoriums] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Building] int  NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [Seats] int  NOT NULL,
    [OpeningDate] nvarchar(max)  NOT NULL,
    [ClosingDate] nvarchar(max)  NOT NULL,
    [DepartmentId] int  NOT NULL
);
GO

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Abbreviation] nvarchar(max)  NOT NULL,
    [FacultyId] int  NOT NULL
);
GO

-- Creating table 'LessonsTypes'
CREATE TABLE [dbo].[LessonsTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LessonsSubTypes'
CREATE TABLE [dbo].[LessonsSubTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [LessonsTypeId] int  NOT NULL
);
GO

-- Creating table 'Rings'
CREATE TABLE [dbo].[Rings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Begin] nvarchar(max)  NOT NULL,
    [End] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [TitleId] int  NOT NULL,
    [FacultyId] int  NOT NULL,
    [DegreeId] int  NOT NULL
);
GO

-- Creating table 'Faculties'
CREATE TABLE [dbo].[Faculties] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Abbreviation] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TestsTypes'
CREATE TABLE [dbo].[TestsTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FieldsOfStudy'
CREATE TABLE [dbo].[FieldsOfStudy] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SessionTypes'
CREATE TABLE [dbo].[SessionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'StudyTypes'
CREATE TABLE [dbo].[StudyTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Positions'
CREATE TABLE [dbo].[Positions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Subjects'
CREATE TABLE [dbo].[Subjects] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Abbreviation] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Degrees'
CREATE TABLE [dbo].[Degrees] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Titles'
CREATE TABLE [dbo].[Titles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RegulatoryActions'
CREATE TABLE [dbo].[RegulatoryActions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Hours] int  NOT NULL,
    [DepartmentId] int  NOT NULL,
    [LessonsTypeId] int  NOT NULL
);
GO

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GroupAbbreviation] nvarchar(max)  NOT NULL,
    [Cource] int  NOT NULL,
    [StudCount] int  NOT NULL,
    [SpecialtyAbbreviation] nvarchar(max)  NOT NULL,
    [StudyTypeId] int  NOT NULL,
    [FacultyId] int  NOT NULL,
    [FieldOfStudyId] int  NOT NULL
);
GO

-- Creating table 'EduPeriods'
CREATE TABLE [dbo].[EduPeriods] (
    [Begin] datetime  NOT NULL,
    [End] datetime  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [GroupId] int  NOT NULL
);
GO

-- Creating table 'TestsPeriods'
CREATE TABLE [dbo].[TestsPeriods] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Begin] nvarchar(max)  NOT NULL,
    [End] nvarchar(max)  NOT NULL,
    [GroupId] int  NOT NULL,
    [SessionTypeId] int  NOT NULL
);
GO

-- Creating table 'Lessons'
CREATE TABLE [dbo].[Lessons] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Period] bit  NOT NULL,
    [RingId] int  NOT NULL,
    [RegulatoryActionId] int  NOT NULL,
    [AuditoriumId] int  NOT NULL,
    [Day] int  NOT NULL
);
GO

-- Creating table 'Validations'
CREATE TABLE [dbo].[Validations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [TestActionId] int  NOT NULL,
    [RingId] int  NOT NULL,
    [AuditoriumId] int  NOT NULL
);
GO

-- Creating table 'Activities'
CREATE TABLE [dbo].[Activities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL,
    [Period] nvarchar(max)  NOT NULL,
    [RingId] int  NOT NULL,
    [AuditoriumId] int  NOT NULL
);
GO

-- Creating table 'AcademicLoadSet'
CREATE TABLE [dbo].[AcademicLoadSet] (
    [EmployeId] int  NOT NULL,
    [RegulatoryActionId] int  NOT NULL
);
GO

-- Creating table 'Curriculums'
CREATE TABLE [dbo].[Curriculums] (
    [RegulatoryActionId] int  NOT NULL,
    [SubjectId] int  NOT NULL,
    [GroupId] int  NOT NULL
);
GO

-- Creating table 'TestCurriculums'
CREATE TABLE [dbo].[TestCurriculums] (
    [EmployeId] int  NOT NULL,
    [TestActionId] int  NOT NULL
);
GO

-- Creating table 'TestActions'
CREATE TABLE [dbo].[TestActions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SubjectId] int  NOT NULL,
    [GroupId] int  NOT NULL,
    [TestsTypeId] int  NOT NULL,
    [DepartmentId] int  NOT NULL
);
GO

-- Creating table 'EmployePosition'
CREATE TABLE [dbo].[EmployePosition] (
    [Employe_Id] int  NOT NULL,
    [Position_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Auditoriums'
ALTER TABLE [dbo].[Auditoriums]
ADD CONSTRAINT [PK_Auditoriums]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LessonsTypes'
ALTER TABLE [dbo].[LessonsTypes]
ADD CONSTRAINT [PK_LessonsTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LessonsSubTypes'
ALTER TABLE [dbo].[LessonsSubTypes]
ADD CONSTRAINT [PK_LessonsSubTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Rings'
ALTER TABLE [dbo].[Rings]
ADD CONSTRAINT [PK_Rings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Faculties'
ALTER TABLE [dbo].[Faculties]
ADD CONSTRAINT [PK_Faculties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TestsTypes'
ALTER TABLE [dbo].[TestsTypes]
ADD CONSTRAINT [PK_TestsTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FieldsOfStudy'
ALTER TABLE [dbo].[FieldsOfStudy]
ADD CONSTRAINT [PK_FieldsOfStudy]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SessionTypes'
ALTER TABLE [dbo].[SessionTypes]
ADD CONSTRAINT [PK_SessionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StudyTypes'
ALTER TABLE [dbo].[StudyTypes]
ADD CONSTRAINT [PK_StudyTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Positions'
ALTER TABLE [dbo].[Positions]
ADD CONSTRAINT [PK_Positions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Subjects'
ALTER TABLE [dbo].[Subjects]
ADD CONSTRAINT [PK_Subjects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Degrees'
ALTER TABLE [dbo].[Degrees]
ADD CONSTRAINT [PK_Degrees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Titles'
ALTER TABLE [dbo].[Titles]
ADD CONSTRAINT [PK_Titles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RegulatoryActions'
ALTER TABLE [dbo].[RegulatoryActions]
ADD CONSTRAINT [PK_RegulatoryActions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EduPeriods'
ALTER TABLE [dbo].[EduPeriods]
ADD CONSTRAINT [PK_EduPeriods]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TestsPeriods'
ALTER TABLE [dbo].[TestsPeriods]
ADD CONSTRAINT [PK_TestsPeriods]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [PK_Lessons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Validations'
ALTER TABLE [dbo].[Validations]
ADD CONSTRAINT [PK_Validations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Activities'
ALTER TABLE [dbo].[Activities]
ADD CONSTRAINT [PK_Activities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [EmployeId], [RegulatoryActionId] in table 'AcademicLoadSet'
ALTER TABLE [dbo].[AcademicLoadSet]
ADD CONSTRAINT [PK_AcademicLoadSet]
    PRIMARY KEY NONCLUSTERED ([EmployeId], [RegulatoryActionId] ASC);
GO

-- Creating primary key on [RegulatoryActionId], [SubjectId], [GroupId] in table 'Curriculums'
ALTER TABLE [dbo].[Curriculums]
ADD CONSTRAINT [PK_Curriculums]
    PRIMARY KEY CLUSTERED ([RegulatoryActionId], [SubjectId], [GroupId] ASC);
GO

-- Creating primary key on [EmployeId], [TestActionId] in table 'TestCurriculums'
ALTER TABLE [dbo].[TestCurriculums]
ADD CONSTRAINT [PK_TestCurriculums]
    PRIMARY KEY NONCLUSTERED ([EmployeId], [TestActionId] ASC);
GO

-- Creating primary key on [Id] in table 'TestActions'
ALTER TABLE [dbo].[TestActions]
ADD CONSTRAINT [PK_TestActions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Employe_Id], [Position_Id] in table 'EmployePosition'
ALTER TABLE [dbo].[EmployePosition]
ADD CONSTRAINT [PK_EmployePosition]
    PRIMARY KEY NONCLUSTERED ([Employe_Id], [Position_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DepartmentId] in table 'Auditoriums'
ALTER TABLE [dbo].[Auditoriums]
ADD CONSTRAINT [FK_AuditoriumDepartment]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[Departments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AuditoriumDepartment'
CREATE INDEX [IX_FK_AuditoriumDepartment]
ON [dbo].[Auditoriums]
    ([DepartmentId]);
GO

-- Creating foreign key on [LessonsTypeId] in table 'LessonsSubTypes'
ALTER TABLE [dbo].[LessonsSubTypes]
ADD CONSTRAINT [FK_LessonsTypeLessonsSubType]
    FOREIGN KEY ([LessonsTypeId])
    REFERENCES [dbo].[LessonsTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LessonsTypeLessonsSubType'
CREATE INDEX [IX_FK_LessonsTypeLessonsSubType]
ON [dbo].[LessonsSubTypes]
    ([LessonsTypeId]);
GO

-- Creating foreign key on [FacultyId] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [FK_FacultyDepartment]
    FOREIGN KEY ([FacultyId])
    REFERENCES [dbo].[Faculties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FacultyDepartment'
CREATE INDEX [IX_FK_FacultyDepartment]
ON [dbo].[Departments]
    ([FacultyId]);
GO

-- Creating foreign key on [Employe_Id] in table 'EmployePosition'
ALTER TABLE [dbo].[EmployePosition]
ADD CONSTRAINT [FK_EmployePosition_Employe]
    FOREIGN KEY ([Employe_Id])
    REFERENCES [dbo].[Employees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Position_Id] in table 'EmployePosition'
ALTER TABLE [dbo].[EmployePosition]
ADD CONSTRAINT [FK_EmployePosition_Position]
    FOREIGN KEY ([Position_Id])
    REFERENCES [dbo].[Positions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployePosition_Position'
CREATE INDEX [IX_FK_EmployePosition_Position]
ON [dbo].[EmployePosition]
    ([Position_Id]);
GO

-- Creating foreign key on [RingId] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [FK_LessonRing]
    FOREIGN KEY ([RingId])
    REFERENCES [dbo].[Rings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LessonRing'
CREATE INDEX [IX_FK_LessonRing]
ON [dbo].[Lessons]
    ([RingId]);
GO

-- Creating foreign key on [DepartmentId] in table 'RegulatoryActions'
ALTER TABLE [dbo].[RegulatoryActions]
ADD CONSTRAINT [FK_DepartmentRegulatoryAction]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[Departments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentRegulatoryAction'
CREATE INDEX [IX_FK_DepartmentRegulatoryAction]
ON [dbo].[RegulatoryActions]
    ([DepartmentId]);
GO

-- Creating foreign key on [StudyTypeId] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [FK_GroupStudyType]
    FOREIGN KEY ([StudyTypeId])
    REFERENCES [dbo].[StudyTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupStudyType'
CREATE INDEX [IX_FK_GroupStudyType]
ON [dbo].[Groups]
    ([StudyTypeId]);
GO

-- Creating foreign key on [GroupId] in table 'TestsPeriods'
ALTER TABLE [dbo].[TestsPeriods]
ADD CONSTRAINT [FK_TestsPeriodGroup]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TestsPeriodGroup'
CREATE INDEX [IX_FK_TestsPeriodGroup]
ON [dbo].[TestsPeriods]
    ([GroupId]);
GO

-- Creating foreign key on [SessionTypeId] in table 'TestsPeriods'
ALTER TABLE [dbo].[TestsPeriods]
ADD CONSTRAINT [FK_TestsPeriodSessionType]
    FOREIGN KEY ([SessionTypeId])
    REFERENCES [dbo].[SessionTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TestsPeriodSessionType'
CREATE INDEX [IX_FK_TestsPeriodSessionType]
ON [dbo].[TestsPeriods]
    ([SessionTypeId]);
GO

-- Creating foreign key on [RegulatoryActionId] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [FK_LessonRegulatoryAction]
    FOREIGN KEY ([RegulatoryActionId])
    REFERENCES [dbo].[RegulatoryActions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LessonRegulatoryAction'
CREATE INDEX [IX_FK_LessonRegulatoryAction]
ON [dbo].[Lessons]
    ([RegulatoryActionId]);
GO

-- Creating foreign key on [AuditoriumId] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [FK_LessonAuditorium]
    FOREIGN KEY ([AuditoriumId])
    REFERENCES [dbo].[Auditoriums]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LessonAuditorium'
CREATE INDEX [IX_FK_LessonAuditorium]
ON [dbo].[Lessons]
    ([AuditoriumId]);
GO

-- Creating foreign key on [TitleId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_TitleEmploye]
    FOREIGN KEY ([TitleId])
    REFERENCES [dbo].[Titles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TitleEmploye'
CREATE INDEX [IX_FK_TitleEmploye]
ON [dbo].[Employees]
    ([TitleId]);
GO

-- Creating foreign key on [EmployeId] in table 'AcademicLoadSet'
ALTER TABLE [dbo].[AcademicLoadSet]
ADD CONSTRAINT [FK_AcademicLoadEmploye]
    FOREIGN KEY ([EmployeId])
    REFERENCES [dbo].[Employees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RegulatoryActionId] in table 'AcademicLoadSet'
ALTER TABLE [dbo].[AcademicLoadSet]
ADD CONSTRAINT [FK_AcademicLoadRegulatoryAction]
    FOREIGN KEY ([RegulatoryActionId])
    REFERENCES [dbo].[RegulatoryActions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AcademicLoadRegulatoryAction'
CREATE INDEX [IX_FK_AcademicLoadRegulatoryAction]
ON [dbo].[AcademicLoadSet]
    ([RegulatoryActionId]);
GO

-- Creating foreign key on [RegulatoryActionId] in table 'Curriculums'
ALTER TABLE [dbo].[Curriculums]
ADD CONSTRAINT [FK_CurriculumRegulatoryAction]
    FOREIGN KEY ([RegulatoryActionId])
    REFERENCES [dbo].[RegulatoryActions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SubjectId] in table 'Curriculums'
ALTER TABLE [dbo].[Curriculums]
ADD CONSTRAINT [FK_CurriculumSubject]
    FOREIGN KEY ([SubjectId])
    REFERENCES [dbo].[Subjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CurriculumSubject'
CREATE INDEX [IX_FK_CurriculumSubject]
ON [dbo].[Curriculums]
    ([SubjectId]);
GO

-- Creating foreign key on [GroupId] in table 'Curriculums'
ALTER TABLE [dbo].[Curriculums]
ADD CONSTRAINT [FK_CurriculumGroup]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CurriculumGroup'
CREATE INDEX [IX_FK_CurriculumGroup]
ON [dbo].[Curriculums]
    ([GroupId]);
GO

-- Creating foreign key on [EmployeId] in table 'TestCurriculums'
ALTER TABLE [dbo].[TestCurriculums]
ADD CONSTRAINT [FK_TestCurriculumEmploye]
    FOREIGN KEY ([EmployeId])
    REFERENCES [dbo].[Employees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SubjectId] in table 'TestActions'
ALTER TABLE [dbo].[TestActions]
ADD CONSTRAINT [FK_TestActionSubject]
    FOREIGN KEY ([SubjectId])
    REFERENCES [dbo].[Subjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TestActionSubject'
CREATE INDEX [IX_FK_TestActionSubject]
ON [dbo].[TestActions]
    ([SubjectId]);
GO

-- Creating foreign key on [GroupId] in table 'TestActions'
ALTER TABLE [dbo].[TestActions]
ADD CONSTRAINT [FK_TestActionGroup]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TestActionGroup'
CREATE INDEX [IX_FK_TestActionGroup]
ON [dbo].[TestActions]
    ([GroupId]);
GO

-- Creating foreign key on [TestsTypeId] in table 'TestActions'
ALTER TABLE [dbo].[TestActions]
ADD CONSTRAINT [FK_TestActionTestsType]
    FOREIGN KEY ([TestsTypeId])
    REFERENCES [dbo].[TestsTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TestActionTestsType'
CREATE INDEX [IX_FK_TestActionTestsType]
ON [dbo].[TestActions]
    ([TestsTypeId]);
GO

-- Creating foreign key on [DepartmentId] in table 'TestActions'
ALTER TABLE [dbo].[TestActions]
ADD CONSTRAINT [FK_TestActionDepartment]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[Departments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TestActionDepartment'
CREATE INDEX [IX_FK_TestActionDepartment]
ON [dbo].[TestActions]
    ([DepartmentId]);
GO

-- Creating foreign key on [TestActionId] in table 'TestCurriculums'
ALTER TABLE [dbo].[TestCurriculums]
ADD CONSTRAINT [FK_TestCurriculumTestAction]
    FOREIGN KEY ([TestActionId])
    REFERENCES [dbo].[TestActions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TestCurriculumTestAction'
CREATE INDEX [IX_FK_TestCurriculumTestAction]
ON [dbo].[TestCurriculums]
    ([TestActionId]);
GO

-- Creating foreign key on [FacultyId] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [FK_FacultyGroup]
    FOREIGN KEY ([FacultyId])
    REFERENCES [dbo].[Faculties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FacultyGroup'
CREATE INDEX [IX_FK_FacultyGroup]
ON [dbo].[Groups]
    ([FacultyId]);
GO

-- Creating foreign key on [FieldOfStudyId] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [FK_FieldOfStudyGroup]
    FOREIGN KEY ([FieldOfStudyId])
    REFERENCES [dbo].[FieldsOfStudy]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FieldOfStudyGroup'
CREATE INDEX [IX_FK_FieldOfStudyGroup]
ON [dbo].[Groups]
    ([FieldOfStudyId]);
GO

-- Creating foreign key on [LessonsTypeId] in table 'RegulatoryActions'
ALTER TABLE [dbo].[RegulatoryActions]
ADD CONSTRAINT [FK_RegulatoryActionLessonsType]
    FOREIGN KEY ([LessonsTypeId])
    REFERENCES [dbo].[LessonsTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RegulatoryActionLessonsType'
CREATE INDEX [IX_FK_RegulatoryActionLessonsType]
ON [dbo].[RegulatoryActions]
    ([LessonsTypeId]);
GO

-- Creating foreign key on [TestActionId] in table 'Validations'
ALTER TABLE [dbo].[Validations]
ADD CONSTRAINT [FK_ValidationTestAction]
    FOREIGN KEY ([TestActionId])
    REFERENCES [dbo].[TestActions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ValidationTestAction'
CREATE INDEX [IX_FK_ValidationTestAction]
ON [dbo].[Validations]
    ([TestActionId]);
GO

-- Creating foreign key on [RingId] in table 'Validations'
ALTER TABLE [dbo].[Validations]
ADD CONSTRAINT [FK_ValidationRing]
    FOREIGN KEY ([RingId])
    REFERENCES [dbo].[Rings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ValidationRing'
CREATE INDEX [IX_FK_ValidationRing]
ON [dbo].[Validations]
    ([RingId]);
GO

-- Creating foreign key on [AuditoriumId] in table 'Validations'
ALTER TABLE [dbo].[Validations]
ADD CONSTRAINT [FK_ValidationAuditorium]
    FOREIGN KEY ([AuditoriumId])
    REFERENCES [dbo].[Auditoriums]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ValidationAuditorium'
CREATE INDEX [IX_FK_ValidationAuditorium]
ON [dbo].[Validations]
    ([AuditoriumId]);
GO

-- Creating foreign key on [RingId] in table 'Activities'
ALTER TABLE [dbo].[Activities]
ADD CONSTRAINT [FK_ActivityRing]
    FOREIGN KEY ([RingId])
    REFERENCES [dbo].[Rings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActivityRing'
CREATE INDEX [IX_FK_ActivityRing]
ON [dbo].[Activities]
    ([RingId]);
GO

-- Creating foreign key on [AuditoriumId] in table 'Activities'
ALTER TABLE [dbo].[Activities]
ADD CONSTRAINT [FK_ActivityAuditorium]
    FOREIGN KEY ([AuditoriumId])
    REFERENCES [dbo].[Auditoriums]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActivityAuditorium'
CREATE INDEX [IX_FK_ActivityAuditorium]
ON [dbo].[Activities]
    ([AuditoriumId]);
GO

-- Creating foreign key on [FacultyId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_EmployeFaculty]
    FOREIGN KEY ([FacultyId])
    REFERENCES [dbo].[Faculties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeFaculty'
CREATE INDEX [IX_FK_EmployeFaculty]
ON [dbo].[Employees]
    ([FacultyId]);
GO

-- Creating foreign key on [GroupId] in table 'EduPeriods'
ALTER TABLE [dbo].[EduPeriods]
ADD CONSTRAINT [FK_EduPeriodGroup]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EduPeriodGroup'
CREATE INDEX [IX_FK_EduPeriodGroup]
ON [dbo].[EduPeriods]
    ([GroupId]);
GO

-- Creating foreign key on [DegreeId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_DegreeEmploye]
    FOREIGN KEY ([DegreeId])
    REFERENCES [dbo].[Degrees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DegreeEmploye'
CREATE INDEX [IX_FK_DegreeEmploye]
ON [dbo].[Employees]
    ([DegreeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------