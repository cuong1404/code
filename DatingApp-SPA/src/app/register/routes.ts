import { Routes } from '@angular/router';
import { HomeComponent } from '../home/home.component';
import { MemberListComponent } from '../member-list/member-list.component';
import { MessagesComponent } from '../messages/messages.component';
import { ListsComponent } from '../lists/lists.component';
import { AuthGuard } from '../_guards/auth.guard';
import { AuthService } from '../_services/auth.service';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent    },
    // custom all sub route to using same canActive
    {
        // path: 'user' => localhost:user/members
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthService],
        children: [
            { path: 'members', component: MemberListComponent    },
            { path: 'messages', component: MessagesComponent  },
            { path: 'lists', component: ListsComponent },
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'    }
];
