﻿/// <reference path="../../../typings/_all.d.ts" />

module Antares.Activity.Commands {
    import Business = Common.Models.Business;
    
    export class ActivityUserCommandPart implements IActivityUserCommandPart {
        userId: string = '';
        callDate: Date = null;

        constructor(activityUser?: Business.ActivityUser) {
            if (activityUser) {
                this.userId = activityUser.userId;
                // Core.DateTimeUtils.convertDateToUtc
                this.callDate = Core.DateTimeUtils.convertDateToUtc(activityUser.callDate);
            }
        }
    }
    
    export interface IActivityUserCommandPart {
        userId: string;
        callDate: Date;
    }
}