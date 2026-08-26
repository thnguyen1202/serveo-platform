import i18n from 'i18next';
import LanguageDetector from 'i18next-browser-languagedetector';
import { initReactI18next } from 'react-i18next';

import enCommon from './locales/en/common.json';

import viCommon from './locales/vi/common.json';

i18n
  .use(LanguageDetector)
  .use(initReactI18next)
  .init({
    lng: 'vi',
    fallbackLng: 'vi',

    supportedLngs: ['en', 'vi'],

    ns: ['common'],
    defaultNS: 'common',

    interpolation: {
      escapeValue: false,
    },

    resources: {
      en: {
        common: enCommon,
      },
      vi: {
        common: viCommon,
      },
    },
  });

export default i18n;
