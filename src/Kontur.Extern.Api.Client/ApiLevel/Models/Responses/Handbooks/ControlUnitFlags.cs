using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
public enum ControlUnitFlags
{
    Unknown = -1,
    IsActive,
    IsTest,
    Gp3IsOn,
    PfrIosIsOn,
    PfrIosFioSnilsIsOn,
    PfrPensionIsOn,
    PfrLettersOnly,
    FnsLettersOnly,
    FnsDemandsOnly,
    FnsSubmissionsOnly,
    Gost2012,
    CempIsOn,
    BusinessRegistration
}