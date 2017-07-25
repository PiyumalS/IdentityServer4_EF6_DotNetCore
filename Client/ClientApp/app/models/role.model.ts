
import { Permission } from './permission.model';


export class Role {

    constructor(name?: string, permissions?: Permission[]) {

        this.name = name;
        this.permissions = permissions;
    }

    public id: string;
    public name: string;
    public permissions: Permission[];
}