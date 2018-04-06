import logging


class Logger:

    @staticmethod
    def add_log(log_file_name='out', class_name='root', log_level=3, log_type='info', message=''):
        log_format = '%(levelname)s - %(message)s'
        if log_level >= 2:
            log_format = '%(name)s - {log_format}'.format(log_format=log_format)
        if log_level == 3:
            log_format = '%(asctime)s - {log_format}'.format(log_format=log_format)

        logger = logging.getLogger(name=class_name)
        logger.setLevel(level=logging.INFO)

        fh = logging.FileHandler(filename='{}.log'.format(log_file_name))
        formatter = logging.Formatter(fmt=log_format)
        fh.setFormatter(fmt=formatter)
        logger.handlers.clear()
        logger.addHandler(fh)

        if log_type == 'info':
            logger.info(msg=message)
        elif log_type == 'warn':
            logger.warning(msg=message)
        elif log_type == 'error':
            logger.error(msg=message)
