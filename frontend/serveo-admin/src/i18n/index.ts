import i18n from 'i18next';
import LanguageDetector from 'i18next-browser-languagedetector';
import { initReactI18next } from 'react-i18next';

import enCommon from './locales/en/common.json';
import enPage from './locales/en/page.json';
import viCommon from './locales/vi/common.json';
import viPage from './locales/vi/page.json';

i18n
  .use(LanguageDetector)
  .use(initReactI18next)
  .init({
    resources: {
      vi: {
        common: viCommon,
        page: enPage,
      },

      en: {
        common: enCommon,
        page: viPage,
      },
    },

    lng: 'vi',
    fallbackLng: 'vi',

    ns: ['common', 'page'],
    defaultNS: 'common',

    detection: {
      order: ['localStorage', 'navigator'],
      caches: ['localStorage'],
    },

    interpolation: {
      escapeValue: false,
    },
  });

export default i18n;
