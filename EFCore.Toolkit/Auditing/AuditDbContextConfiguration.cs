﻿
using System;

namespace EFCore.Toolkit.Auditing
{
    public class AuditDbContextConfiguration
    {
        public AuditDbContextConfiguration(bool auditEnabled, DateTimeKind auditDateTimeKind = DateTimeKind.Utc, params AuditTypeInfo[] auditTypeInfos)
        {
            this.AuditEnabled = auditEnabled;
            this.AuditDateTimeKind = auditDateTimeKind;
            this.AuditTypeInfos = auditTypeInfos;
        }

        public bool AuditEnabled { get; }

        public DateTimeKind AuditDateTimeKind { get; }

        public AuditTypeInfo[] AuditTypeInfos { get; }

    }
}