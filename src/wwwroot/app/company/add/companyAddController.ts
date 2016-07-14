/// <reference path="../../typings/_all.d.ts" />

module Antares.Company {

    import Business = Common.Models.Business;
    import Dto = Common.Models.Dto;

    export class CompanyAddController extends Core.WithPanelsBaseController  {
        company: Business.Company;
        private companyResource: Common.Models.Resources.ICompanyResourceClass;

        private descriptionMaxLength: number = CompanyControls.descriptionMaxLength;
        private config = CompanyControls.config;
        private controlSchemas = CompanyControls.controlSchemas;

        private addCompanyForm: ng.IFormController | any;

        constructor(
            componentRegistry: Core.Service.ComponentRegistry,
            private dataAccessService: Services.DataAccessService,
            private enumService: Services.EnumService,
            private $scope: ng.IScope,
            private $q: ng.IQService,
            private $state: ng.ui.IStateService) {

            super(componentRegistry, $scope);

            this.company = new Business.Company();

            this.companyResource = dataAccessService.getCompanyResource();
        }
        
        $postLink() {
            this.addCompanyForm = this.$scope["addCompanyForm"];

            this.updateContactsValidity();
        }

        hasCompanyContacts = (): boolean => {
            return this.company.contacts != null && this.company.contacts.length > 0;
        }

        showContactList = () => {            
            var selectedContactIds: string[] = this.hasCompanyContacts() ? this.company.contacts.map((contact: Dto.IContact) => { return contact.id }) : [];

            this.components.contactList()
                .loadContacts()
                .then(() => {                    
                    this.components.contactList().setSelected(selectedContactIds);
                    this.components.sidePanels.contact().show();
                });
        }

        cancelUpdateContacts = () => {
            this.components.sidePanels.contact().hide();
        }

        updateContacts = () => {
            var selectedContacts = this.components.contactList().getSelected();            
            this.company.contacts = selectedContacts.map((contact: Dto.IContact) => { return new Business.Contact(contact) });
            this.components.sidePanels.contact().hide();

            this.updateContactsValidity();
        }

        updateContactsValidity = () =>{
            let areContactsValid = this.hasCompanyContacts();
            this.addCompanyForm.$setValidity("company.contacts.custom", areContactsValid);
        }
   
        createCompany = () => {
            this.company.websiteUrl = CompanyControls.formatUrlWithProtocol(this.company.websiteUrl);
            this.company.clientCarePageUrl = CompanyControls.formatUrlWithProtocol(this.company.clientCarePageUrl);

            this.companyResource
                .save(new Business.CreateCompanyResource(this.company))
                .$promise
                .then((company: Dto.ICompany) => {
                   this.company = new Business.Company();
                    var form = this.$scope["addCompanyForm"];
                    form.$setPristine();
                    this.$state.go('app.company-view', company);
                });
        }

        defineComponentIds() {
            this.componentIds = {
                contactSidePanelId: 'addCompany:contactSidePanelComponent',
                contactListId: 'addCompany:contactListComponent'
            }
        }

        defineComponents() {
            this.components = {
                sidePanels: {
                    contact: () => { return this.componentRegistry.get(this.componentIds.contactSidePanelId); },
                },
                contactList: () => { return this.componentRegistry.get(this.componentIds.contactListId); }            
            };
        }

    }

    angular.module('app').controller('CompanyAddController', CompanyAddController);
}