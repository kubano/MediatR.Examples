/// <reference path="../../../../typings/_all.d.ts" />

module Antares.Common.Component {
    import Business = Common.Models.Business;
    import Dto = Common.Models.Dto;
    import Enums = Common.Models.Enums;
    import DepartmentUserResourceParameters = Common.Models.Resources.IDepartmentUserResourceParameters;

    export class NegotiatorsController {
        public activityId: string;
        public propertyDivisionId: string;
        public leadNegotiator: Business.ActivityUser;
        public secondaryNegotiators: Business.ActivityUser[];

        public isLeadNegotiatorInEditMode: boolean = false;
        public isSecondaryNegotiatorsInEditMode: boolean = false;

        public negotiatorsSearchOptions: SearchOptions = new SearchOptions();
        public labelTranslationKey: string;
        public nagotiatorCallDateOpened: any = {};

        public today: Date = new Date();

        private usersSearchMaxCount: number = 100;

        constructor(
            private dataAccessService: Services.DataAccessService,
            private enumService: Services.EnumService) {

            this.enumService.getEnumPromise().then(this.onEnumLoaded);
        }

        onEnumLoaded = (result: any) => {
            var divisions: any = result[Dto.EnumTypeCode.Division];
            var division: any = _.find(divisions, { 'id': this.propertyDivisionId });
            if (division){
                this.labelTranslationKey = division.code.toUpperCase();
            }
        }

        public editLeadNegotiator = () => {
            this.isLeadNegotiatorInEditMode = true;
        }

        public changeLeadNegotiator = (user: Dto.IDepartmentUser) => {
            this.leadNegotiator = this.createActivityUser(user, Enums.NegotiatorTypeEnum.LeadNegotiator);

            this.isLeadNegotiatorInEditMode = false;
        }

        public cancelChangeLeadNegotiator = () => {
            this.isLeadNegotiatorInEditMode = false;
        }

        public editSecondaryNegotiators = () => {
            this.isSecondaryNegotiatorsInEditMode = true;
        }

        public addSecondaryNegotiator = (user: Dto.IDepartmentUser) => {
            this.secondaryNegotiators.push(this.createActivityUser(user, Enums.NegotiatorTypeEnum.SecondaryNegotiator));
        }

        public deleteSecondaryNegotiator = (activityUser: Business.ActivityUser) => {
            _.remove(this.secondaryNegotiators, (itm) => itm.userId === activityUser.userId);
        }

        public cancelAddSecondaryNegotiator = () => {
            this.isSecondaryNegotiatorsInEditMode = false;
        }

        public switchToLeadNegotiator = (activityUser: Business.ActivityUser) =>{
            _.remove(this.secondaryNegotiators, (itm) => itm.userId === activityUser.userId);
            this.secondaryNegotiators.push(this.leadNegotiator);

            this.leadNegotiator = activityUser;
        }

        public openNegotiatorCallDate = (nagotiatorUserId: string) => {
            this.nagotiatorCallDateOpened[nagotiatorUserId] = true;
        }

        public getUsersQuery = (searchValue: string): DepartmentUserResourceParameters => {
            var excludedIds: string[] = _.map<Business.ActivityUser, string>(this.secondaryNegotiators, 'userId');
            excludedIds.push(this.leadNegotiator.userId);

            return { partialName : searchValue, take : this.usersSearchMaxCount, 'excludedIds[]' : excludedIds };
        }

        public getUsers = (searchValue: string) =>{
            var query = this.getUsersQuery(searchValue);

            return this.dataAccessService
                .getDepartmentUserResource()
                .query(query)
                .$promise
                .then((users: any) => {
                    return users.map((user: Common.Models.Dto.IDepartmentUser) => { return new Common.Models.Business.DepartmentUser(<Common.Models.Dto.IDepartmentUser>user); });
                });
        }

        public isSubmitted = (form: any) => {
            while (!!form) {
                if (form.$submitted) return true;
                form = form.$$parentForm;
            }
            return false;
        };

        private createActivityUser = (user: Dto.IDepartmentUser, negotiatorType: Enums.NegotiatorTypeEnum) =>{
            var activityUser = new Business.ActivityUser();
            activityUser.activityId = this.activityId;
            activityUser.userId = user.id;
            activityUser.user = new Business.DepartmentUser(<Dto.IUser>user);

            return activityUser;
        }
    }

    angular.module('app').controller('NegotiatorsController', NegotiatorsController);
}