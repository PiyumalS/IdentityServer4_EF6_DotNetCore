import 'core-js/client/shim';
import 'reflect-metadata';
import 'zone.js/dist/zone';
import 'hammerjs';
import './styles/blue-amber.scss';
import './styles/app.scss';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app.module';
if (module.hot) {
    module.hot.accept();
}
platformBrowserDynamic().bootstrapModule(AppModule);
//# sourceMappingURL=main.js.map