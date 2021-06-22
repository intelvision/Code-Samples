import {CommonTableApiInterface} from 'src/modules/core/services/http/common-table-api.interface';
import {MatDialog} from '@angular/material/dialog';
import {PaginationSearchModel} from '../../../core/models/http/helpers/pagination-search.model';
import {MatTableDataSource} from '@angular/material/table';
import {debounceTime} from 'rxjs/operators';
import {PageEvent} from '@angular/material/paginator';
import {SortEventModel} from '../../../core/models/helpers/table/sort-event.model';
import {ConfirmationDialogComponent} from '../confirmation-dialog/confirmation-dialog.component';
import {TableColumnModel} from '../../../core/models/helpers/table/table-column.model';
import {FormControl} from '@angular/forms';
import {OnInit} from '@angular/core';
import {ComponentType} from '@angular/cdk/overlay';
import {DialogType} from '../../../core/models/helpers/dialog-type.enum';
import {AuthService} from '../../../core/services/http/auth/auth.service';

export class CommonTableBusinessLogic<TGetDetailedModel, TListItemModel, TCreateModel, TUpdateModel, DialogModel> implements OnInit {


    tableSource = new MatTableDataSource<TListItemModel>();
    entitiesTotalCount = 0;
    currentFilter = {
        pageSize: 10,
        page: 0,
        direction: 'Asc',
        active: 'Id',
        searchCondition: ''
    } as PaginationSearchModel;
    searchFormControl = new FormControl('');

    constructor(private apiService: CommonTableApiInterface<TGetDetailedModel, TListItemModel, TCreateModel, TUpdateModel>,
                private matDialog: MatDialog,
                private dialogComponentRef: ComponentType<DialogModel>,
                public displayingColumns: Array<TableColumnModel>,
                private loginService: AuthService) {

    }

    ngOnInit(): void {
        this.loadEntities();
        this.subscribeOnSearchChange();
    }

    loadEntities(pagination: PaginationSearchModel = this.currentFilter) {
        this.apiService.getPage(pagination).subscribe(res => {
            this.tableSource = new MatTableDataSource<TListItemModel>(res.data);
            this.entitiesTotalCount = res.total;
        });
    }

    subscribeOnSearchChange() {
        this.searchFormControl.valueChanges.pipe(debounceTime(500)).subscribe(res => {
            this.currentFilter.searchCondition = res;
            this.loadEntities();
        });
    }

    onPaginate(pageEvent: PageEvent) {
        this.currentFilter.page = pageEvent.pageIndex;
        this.currentFilter.pageSize = pageEvent.pageSize;
        this.loadEntities();
    }

    onSort(sortEvent: SortEventModel) {
        this.currentFilter.active = sortEvent.active;
        this.currentFilter.direction = sortEvent.direction;
        this.loadEntities();
    }

    onAddClick() {
        const createDialog = this.matDialog.open(this.dialogComponentRef, {
            height: '60vh',
            width: '50vw',
            disableClose: true,
            data: {
                dialogType: DialogType.Create
            }
        });
        createDialog.afterClosed().subscribe(res => {
            if (res) {
                this.apiService.create(res).subscribe(() => {
                    this.loadEntities();
                });
            }
        });
    }

    onEditClick(entityToEdit: TListItemModel) {
        // @ts-ignore
        this.apiService.getById(entityToEdit.id).subscribe(detailedEntity => {
            const createDialog = this.matDialog.open(this.dialogComponentRef, {
                height: '60vh',
                width: '50vw',
                disableClose: true,
                data: {
                    model: detailedEntity,
                    dialogType: DialogType.Edit
                }
            });
            createDialog.afterClosed().subscribe(res => {
                if (res) {
                    this.apiService.update(res.id, res).subscribe(() => {
                        this.loadEntities();
                    });
                }
            });
        });
    }

    onViewClick(entityToEdit: TListItemModel) {
        // @ts-ignore
        this.apiService.getById(entityToEdit.id).subscribe(detailedEntity => {
            const createDialog = this.matDialog.open(this.dialogComponentRef, {
                height: '60vh',
                width: '50vw',
                disableClose: true,
                data: {
                    model: detailedEntity,
                    dialogType: DialogType.View
                }
            });
            createDialog.afterClosed().subscribe(res => {
                if (res) {
                    this.apiService.update(res.id, res).subscribe(() => {
                        this.loadEntities();
                    });
                }
            });
        });
    }

    onLoginAsClick(entityToLoginAs: TListItemModel) {

        this.loginService.impersonateLogin({
            // @ts-ignore
            login: entityToLoginAs.login
        }).subscribe(res => {
            location.reload();
        });
    }

    onDeleteClick(entityToDelete: TListItemModel) {
        const deleteConfirmationDialog = this.matDialog.open(ConfirmationDialogComponent, {
            width: '20vw',
            height: '20vh',
            data: {
                question: 'Are you sure want to delete entity?'
            }
        });
        deleteConfirmationDialog.afterClosed().subscribe(isApproved => {
            if (isApproved) {
                // @ts-ignore
                this.apiService.delete(entityToDelete.id).subscribe(res => {
                    this.loadEntities();
                });
            }
        });
    }
}
