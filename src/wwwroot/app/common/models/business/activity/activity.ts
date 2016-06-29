/// <reference path="../../../../typings/_all.d.ts" />

module Antares.Common.Models.Business {
    export class Activity implements Dto.IActivity {
        id: string = '';
        propertyId: string = '';
        activityStatusId: string = '';
        activityTypeId: string = '';
        activityType: Dto.IActivityType = null;
        contacts: Contact[] = [];
        attachments: Attachment[] = [];
        property: PreviewProperty = null;
        createdDate: Date = null;
        marketAppraisalPrice: number = null;
        recommendedPrice: number = null;
        vendorEstimatedPrice: number = null;
        viewingsByDay: ViewingGroup[];
        viewings: Viewing[];
        leadNegotiator: ActivityUser = null;
        secondaryNegotiator: ActivityUser[] = [];
        activityUsers: ActivityUser[] = [];
        activityDepartments: ActivityDepartment[] = [];
        offers: Offer[];
        askingPrice: number = null;
        shortLetPricePerWeek: number = null;
        activitySourceId: string = '';
        activitySourceDescription: string = '';
        activitySellingReasonId: string = '';
        pitchningThreats: string = '';
        keyNumber: string = '';
        accessArrangements: string = '';
        invitationText: string = '';
        attendees: Dto.IActivityAttendee[] = [];

        constructor(activity?: Dto.IActivity) {
            if (activity) {
                angular.extend(this, activity);
                this.createdDate = Core.DateTimeUtils.convertDateToUtc(activity.createdDate);
                if (activity.contacts) {
                    this.contacts = activity.contacts.map((contact: Dto.IContact) => { return new Contact(contact) });
                }
                this.property = new PreviewProperty(activity.property);

                var activityleadNegotiator = _.find(activity.activityUsers,
                    (user: Dto.IActivityUser) => user.userType.code === Enums.NegotiatorTypeEnum[Enums.NegotiatorTypeEnum.LeadNegotiator]);
                this.leadNegotiator = new ActivityUser(activityleadNegotiator);

                this.secondaryNegotiator = _.filter(activity.activityUsers,
                    (user: Dto.IActivityUser) => user.userType.code === Enums.NegotiatorTypeEnum[Enums.NegotiatorTypeEnum.SecondaryNegotiator])
                    .map((user: Dto.IActivityUser) => new ActivityUser(user));

                if (activity.activityDepartments) {
                    this.activityDepartments = activity.activityDepartments.map((department: Dto.IActivityDepartment) => { return new Business.ActivityDepartment(department) });
                }

                if (activity.attachments) {
                    this.attachments = activity.attachments.map((attachment: Dto.IAttachment) => { return new Business.Attachment(attachment) });
                }
                else {
                    this.attachments = [];
                }

                if (activity.viewings) {
                    this.viewings = activity.viewings.map((item) => new Viewing(item));
                    this.groupViewings(this.viewings);
                }

                if (activity.offers) {
                    this.offers = activity.offers.map((item) => new Offer(item));
                }
            }

            this.secondaryNegotiator = this.secondaryNegotiator ? this.secondaryNegotiator : [];
        }

        groupViewings(viewings: Viewing[]) {
            this.viewingsByDay = [];
            var groupedViewings: _.Dictionary<Viewing[]> = _.groupBy(viewings, (i: Viewing) => { return i.day; });
            this.viewingsByDay = <ViewingGroup[]>_.map(groupedViewings, (items: Viewing[], key: string) => { return new ViewingGroup(key, items); });
        }
    }
}