import {CommonModule} from '@angular/common';
import {Component, OnDestroy, OnInit} from '@angular/core';
import {AvatarModule} from 'primeng/avatar';
import {AvatarGroupModule} from 'primeng/avatargroup';
import {BadgeModule} from 'primeng/badge';
import {ButtonModule} from 'primeng/button';
import {ChipModule} from 'primeng/chip';
import {OverlayBadgeModule} from 'primeng/overlaybadge';
import {ProgressBarModule} from 'primeng/progressbar';
import {ScrollPanelModule} from 'primeng/scrollpanel';
import {ScrollTopModule} from 'primeng/scrolltop';
import {SkeletonModule} from 'primeng/skeleton';
import {TagModule} from 'primeng/tag';
import {MeterGroupModule} from 'primeng/metergroup';

@Component({
    selector: 'app-misc-demo',
    standalone: true,
    imports: [CommonModule, ProgressBarModule, BadgeModule, AvatarModule, ScrollPanelModule, TagModule, ChipModule, ButtonModule, SkeletonModule, AvatarGroupModule, ScrollTopModule, OverlayBadgeModule, MeterGroupModule],
    template: `
        <div class="card">
            <div class="font-semibold text-xl mb-4">ProgressBar</div>
            <div class="flex flex-col md:flex-row gap-4">
                <div class="md:w-1/2">
                    <p-progressbar [value]="value" [showValue]="true"></p-progressbar>
                </div>
                <div class="md:w-1/2">
                    <p-progressbar [value]="50" [showValue]="false"></p-progressbar>
                </div>
            </div>
        </div>

        <div class="flex flex-col md:flex-row gap-8">
            <div class="md:w-1/2">
                <div class="card">
                    <div class="font-semibold text-xl mb-4">Badge</div>
                    <div class="flex gap-2">
                        <p-badge value="2"></p-badge>
                        <p-badge value="8" severity="success"></p-badge>
                        <p-badge value="4" severity="info"></p-badge>
                        <p-badge value="12" severity="warn"></p-badge>
                        <p-badge value="3" severity="danger"></p-badge>
                    </div>

                    <div class="font-semibold my-4">Overlay</div>
                    <div class="flex gap-6">
                        <p-overlaybadge value="2">
                            <i class="pi pi-bell" style="font-size: 2rem"></i>
                        </p-overlaybadge>
                        <p-overlaybadge value="4" severity="danger">
                            <i class="pi pi-calendar" style="font-size: 2rem"></i>
                        </p-overlaybadge>
                        <p-overlaybadge severity="danger">
                            <i class="pi pi-envelope" style="font-size: 2rem"></i>
                        </p-overlaybadge>
                    </div>

                    <div class="font-semibold my-4">Button</div>
                    <div class="flex gap-2">
                        <p-button label="Emails" badge="8"></p-button>
                        <p-button label="Messages" icon="pi pi-users" severity="warn" badge="8" badgeSeverity="danger"></p-button>
                    </div>

                    <div class="font-semibold my-4">Sizes</div>
                    <div class="flex items-start gap-2">
                        <p-badge [value]="2"></p-badge>
                        <p-badge [value]="4" badgeSize="large" severity="warn"></p-badge>
                        <p-badge [value]="6" badgeSize="xlarge" severity="success"></p-badge>
                    </div>
                </div>

                <div class="card">
                    <div class="font-semibold text-xl mb-4">Avatar</div>
                    <div class="font-semibold mb-4">Group</div>
                    <p-avatar-group class="mb-4">
                        <p-avatar image="/demo/images/avatar/amyelsner.png" size="large" shape="circle"></p-avatar>
                        <p-avatar image="/demo/images/avatar/asiyajavayant.png" size="large" shape="circle"></p-avatar>
                        <p-avatar image="/demo/images/avatar/onyamalimba.png" size="large" shape="circle"></p-avatar>
                        <p-avatar image="/demo/images/avatar/ionibowcher.png" size="large" shape="circle"></p-avatar>
                        <p-avatar image="/demo/images/avatar/xuxuefeng.png" size="large" shape="circle"></p-avatar>
                        <p-avatar
                            label="+2"
                            shape="circle"
                            size="large"
                            [style]="{
                                'background-color': '#9c27b0',
                                color: '#ffffff'
                            }"
                        ></p-avatar>
                    </p-avatar-group>

                    <div class="font-semibold my-4">Label - Circle</div>
                    <p-avatar class="mr-2" label="P" size="xlarge" shape="circle"></p-avatar>
                    <p-avatar
                        class="mr-2"
                        label="V"
                        size="large"
                        [style]="{
                            'background-color': '#2196F3',
                            color: '#ffffff'
                        }"
                        shape="circle"
                    ></p-avatar>
                    <p-avatar
                        class="mr-2"
                        label="U"
                        [style]="{
                            'background-color': '#9c27b0',
                            color: '#ffffff'
                        }"
                        shape="circle"
                    ></p-avatar>

                    <div class="font-semibold my-4">Icon - Badge</div>
                    <p-overlaybadge value="4" severity="danger" class="inline-flex">
                        <p-avatar label="U" size="xlarge" />
                    </p-overlaybadge>
                </div>

                <div class="card">
                    <div class="font-semibold text-xl mb-4">SccrollTop</div>
                    <div style="height: 200px; overflow: auto">
                        <p>
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Vitae et leo duis ut diam. Ultricies mi quis hendrerit dolor magna eget est lorem. Amet
                            consectetur adipiscing elit ut. Nam libero justo laoreet sit amet. Pharetra massa massa ultricies mi quis hendrerit dolor magna. Est ultricies integer quis auctor elit sed vulputate. Consequat ac felis donec et. Tellus
                            orci ac auctor augue mauris. Semper feugiat nibh sed pulvinar proin gravida hendrerit lectus a. Tincidunt arcu non sodales neque sodales. Metus aliquam eleifend mi in nulla posuere sollicitudin aliquam ultrices. Sodales ut
                            etiam sit amet nisl purus. Cursus sit amet dictum sit amet. Tristique senectus et netus et malesuada fames ac turpis egestas. Et tortor consequat id porta nibh venenatis cras sed. Diam maecenas ultricies mi eget mauris.
                            Eget egestas purus viverra accumsan in nisl nisi. Suscipit adipiscing bibendum est ultricies integer. Mattis aliquam faucibus purus in massa tempor nec. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
                            eiusmod tempor incididunt ut labore et dolore magna aliqua. Vitae et leo duis ut diam. Ultricies mi quis hendrerit dolor magna eget est lorem. Amet consectetur adipiscing elit ut. Nam libero justo laoreet sit amet.
                            Pharetra massa massa ultricies mi quis hendrerit dolor magna. Est ultricies integer quis auctor elit sed vulputate. Consequat ac felis donec et. Tellus orci ac auctor augue mauris. Semper feugiat nibh sed pulvinar proin
                            gravida hendrerit lectus a. Tincidunt arcu non sodales neque sodales. Metus aliquam eleifend mi in nulla posuere sollicitudin aliquam ultrices. Sodales ut etiam sit amet nisl purus. Cursus sit amet dictum sit amet.
                            Tristique senectus et netus et malesuada fames ac turpis egestas. Et tortor consequat id porta nibh venenatis cras sed. Diam maecenas ultricies mi eget mauris. Eget egestas purus viverra accumsan in nisl nisi. Suscipit
                            adipiscing bibendum est ultricies integer. Mattis aliquam faucibus purus in massa tempor nec.
                        </p>
                        <p-scrolltop target="parent" [threshold]="100" icon="pi pi-arrow-up" [buttonProps]="{ severity: 'contrast', raised: true, rounded: true }" />
                    </div>
                </div>

                <div class="card">
                    <div class="font-semibold text-xl mb-4">MeterGroup</div>
                    <p-metergroup [value]="meterGroupValue" />
                </div>
            </div>
            <div class="md:w-1/2">
                <div class="card">
                    <div class="font-semibold text-xl mb-4">Tag</div>
                    <div class="font-semibold mb-4">Default</div>
                    <div class="flex gap-2">
                        <p-tag value="Primary"></p-tag>
                        <p-tag severity="success" value="Success"></p-tag>
                        <p-tag severity="info" value="Info"></p-tag>
                        <p-tag severity="warn" value="Warning"></p-tag>
                        <p-tag severity="danger" value="Danger"></p-tag>
                    </div>

                    <div class="font-semibold my-4">Pills</div>
                    <div class="flex gap-2">
                        <p-tag value="Primary" [rounded]="true"></p-tag>
                        <p-tag severity="success" value="Success" [rounded]="true"></p-tag>
                        <p-tag severity="info" value="Info" [rounded]="true"></p-tag>
                        <p-tag severity="warn" value="Warning" [rounded]="true"></p-tag>
                        <p-tag severity="danger" value="Danger" [rounded]="true"></p-tag>
                    </div>

                    <div class="font-semibold my-4">Icons</div>
                    <div class="flex gap-2">
                        <p-tag icon="pi pi-user" value="Primary"></p-tag>
                        <p-tag icon="pi pi-check" severity="success" value="Success"></p-tag>
                        <p-tag icon="pi pi-info-circle" severity="info" value="Info"></p-tag>
                        <p-tag icon="pi pi-exclamation-triangle" severity="warn" value="Warning"></p-tag>
                        <p-tag icon="pi pi-times" severity="danger" value="Danger"></p-tag>
                    </div>
                </div>

                <div class="card">
                    <div class="font-semibold text-xl mb-4">Chip</div>
                    <div class="font-semibold mb-4">Basic</div>
                    <div class="flex items-center flex-col sm:flex-row gap-2">
                        <p-chip label="Action"></p-chip>
                        <p-chip label="Comedy"></p-chip>
                        <p-chip label="Mystery"></p-chip>
                        <p-chip label="Thriller" [removable]="true"></p-chip>
                    </div>

                    <div class="font-semibold my-4">Icon</div>
                    <div class="flex items-center flex-col sm:flex-row gap-2">
                        <p-chip label="Apple" icon="pi pi-apple"></p-chip>
                        <p-chip label="Facebook" icon="pi pi-facebook"></p-chip>
                        <p-chip label="Google" icon="pi pi-google"></p-chip>
                        <p-chip label="Microsoft" icon="pi pi-microsoft" [removable]="true"></p-chip>
                    </div>

                    <div class="font-semibold my-4">Image</div>
                    <div class="flex items-center flex-col sm:flex-row gap-2">
                        <p-chip label="Amy Elsner">
                            <img src="/demo/images/avatar/amyelsner.png" class="w-8 h-8" alt="avatar" />
                        </p-chip>
                        <p-chip label="Asiya Javayant">
                            <img src="/demo/images/avatar/asiyajavayant.png" class="w-8 h-8" alt="avatar" />
                        </p-chip>
                        <p-chip label="Onyama Limba">
                            <img src="/demo/images/avatar/onyamalimba.png" class="w-8 h-8" alt="avatar" />
                        </p-chip>
                        <p-chip label="Xuxue Feng" [removable]="true">
                            <img src="/demo/images/avatar/xuxuefeng.png" class="w-8 h-8" alt="avatar" />
                        </p-chip>
                    </div>
                </div>

                <div class="card">
                    <div class="font-semibold text-xl mb-4">Skeleton</div>
                    <div class="rounded-border border border-surface p-12">
                        <div class="flex mb-4">
                            <p-skeleton shape="circle" size="4rem" styleClass="mr-2"></p-skeleton>
                            <div>
                                <p-skeleton width="10rem" styleClass="mb-2"></p-skeleton>
                                <p-skeleton width="5rem" styleClass="mb-2"></p-skeleton>
                                <p-skeleton height=".5rem"></p-skeleton>
                            </div>
                        </div>
                        <p-skeleton width="100%" height="150px"></p-skeleton>
                        <div class="flex justify-between mt-6">
                            <p-skeleton width="4rem" height="2rem"></p-skeleton>
                            <p-skeleton width="4rem" height="2rem"></p-skeleton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `
})
export class MiscDemo implements OnInit, OnDestroy {
    value = 0;

    interval: any;

    meterGroupValue = [
        { label: 'Apps', color: '#34d399', value: 16 },
        { label: 'Messages', color: '#fbbf24', value: 8 },
        { label: 'Media', color: '#60a5fa', value: 24 },
        { label: 'System', color: '#c084fc', value: 10 }
    ];

    ngOnInit() {
        this.interval = setInterval(() => {
            this.value = this.value + Math.floor(Math.random() * 10) + 1;
            if (this.value >= 100) {
                this.value = 100;
                clearInterval(this.interval);
            }
        }, 2000);
    }

    ngOnDestroy() {
        clearInterval(this.interval);
    }
}
