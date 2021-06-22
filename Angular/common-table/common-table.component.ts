import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {MatTableDataSource} from '@angular/material';
import {TableColumnModel} from '../../../core/models/helpers/table/table-column.model';
import {TableColumnType} from '../../../core/models/helpers/table/table-column-type.enum';
import {TableActionType} from '../../../core/models/helpers/table/table-action-type.enum';
import {PageEvent} from '@angular/material/paginator';
import {SortEventModel} from '../../../core/models/helpers/table/sort-event.model';

@Component({
    selector: 'app-common-table',
    templateUrl: './common-table.component.html',
    styleUrls: ['./common-table.component.scss']
})
export class CommonTableComponent implements OnInit {
    @Input() dataSource = new MatTableDataSource();
    @Input() displayedColumns = new Array<TableColumnModel>();
    @Input() totalItemsCount = 0;
    @Output() edit = new EventEmitter<any>();
    @Output() view = new EventEmitter<any>();
    @Output() delete = new EventEmitter<any>();
    @Output() loginAs = new EventEmitter<any>();

    @Output() paginate = new EventEmitter<PageEvent>();
    @Output() sort = new EventEmitter<SortEventModel>();
    allActionTypes = TableActionType;

    constructor() {
    }

    get commonColumns(): Array<TableColumnModel> {
        return this.displayedColumns.filter(x => x.columnType === TableColumnType.text);
    }

    get commonColumnsTitles(): Array<string> {
        return this.commonColumns.map(x => x.columnTitle);
    }

    get enumToTextColumns(): Array<TableColumnModel> {
        return this.displayedColumns.filter(x => x.columnType === TableColumnType.enumToText);
    }

    get enumToTextColumnTitles(): Array<string> {
        return this.enumToTextColumns.map(x => x.columnTitle);
    }

    get actionColumns(): Array<TableColumnModel> {
        return this.displayedColumns.filter(x => x.columnType === TableColumnType.action);
    }

    get actionColumnsTitles(): Array<string> {
        return this.actionColumns.map(x => x.columnTitle);
    }

    get iconColumns(): Array<TableColumnModel> {
        return this.displayedColumns.filter(x => x.columnType === TableColumnType.icon);
    }

    get iconColumnsTitles(): Array<string> {
        return this.iconColumns.map(x => x.columnTitle);
    }

    get allTitles(): Array<string> {
        return [...this.commonColumnsTitles, ...this.iconColumnsTitles, ...this.enumToTextColumnTitles, ...this.actionColumnsTitles];
    }

    getValueBasedOnPath(object, path: string) {
        const pathParts = path.split('.');
        let objectValue = object[pathParts[0]];
        pathParts.splice(0, 1);
        if (pathParts.length >= 1) {
            pathParts.forEach(part => objectValue = objectValue[part]);
        }

        return objectValue;
    }

    ngOnInit() {
    }

    enumToString(enumCoreTranslationNode: string, enumType, enumValue): string {
        return enumCoreTranslationNode + enumType[enumValue.toString()];
    }

    onTableSort(active: string, direction: string) {
        this.sort.emit({
            active: this.displayedColumns.find(x => x.columnTitle === active).columnProperty,
            direction
        });
    }

    onView(row: any) {
        this.view.emit(row);
    }

    onEdit(row: any) {
        this.edit.emit(row);
    }

    onDelete(row: any) {
        this.delete.emit(row);
    }

    onLoginAs(row: any) {
        this.loginAs.emit(row);
    }

    onPaginationChange(event: PageEvent) {
        this.paginate.emit(event);
    }
}
