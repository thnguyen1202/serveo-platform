import { useTranslation } from 'react-i18next';

export function LanguageSwitcher() {
  const { i18n } = useTranslation();

  function change(lang: string) {
    i18n.changeLanguage(lang);
  }

  return (
    <>
      <button onClick={() => change('vi')}>VN</button>

      <button onClick={() => change('en')}>EN</button>
    </>
  );
}
